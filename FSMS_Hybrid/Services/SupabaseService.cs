using Supabase;

namespace FSMS_Hybrid.Services
{
    public class SupabaseService
    {
        private readonly Client _client;

        public SupabaseService()
        {
            var options = new Supabase.SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true,
                SessionHandler = new MauiSessionPersistor()
            };

            _client = new Client(Constants.SupabaseUrl, Constants.SupabaseKey, options);
        }

        public async Task InitializeAsync()
        {
            DebugLogger.Log("[SupabaseService] Calling _client.InitializeAsync()...");
            await _client.InitializeAsync();
             DebugLogger.Log($"[SupabaseService] Initialized. Auth State: {_client.Auth.CurrentSession?.User?.Email ?? "No User"}");

            // Manual recovery fallback if autoloader failed
            if (_client.Auth.CurrentSession == null)
            {
                 DebugLogger.Log("[SupabaseService] No session after init. Attempting manual restore...");
                 var persistor = new MauiSessionPersistor();
                 var session = persistor.LoadSession();
                 if (session != null)
                 {
                     DebugLogger.Log($"[SupabaseService] Manual restore found session for {session.User?.Email}. Re-hydrating Auth...");
                     try
                     {
                         // Force the client to use this session
                         // Note: We might need to refresh it if expired, but SetSession usually handles state
                         if (!string.IsNullOrEmpty(session.AccessToken) && !string.IsNullOrEmpty(session.RefreshToken))
                         {
                             await _client.Auth.SetSession(session.AccessToken, session.RefreshToken);
                             DebugLogger.Log("[SupabaseService] Manual Re-hydration SUCCESS!");
                         }
                     }
                     catch (Exception ex)
                     {
                         DebugLogger.Log($"[SupabaseService] Manual Re-hydration FAILED: {ex.Message}");
                     }
                 }
                 else
                 {
                      DebugLogger.Log("[SupabaseService] Manual restore found NOTHING.");
                 }
            }
        }

        public Client Client => _client;

        // wrapper for login
        public async Task<(bool Success, string Message)> LoginAsync(string email, string password)
        {
            try
            {
                var session = await _client.Auth.SignIn(email, password);
                return (session != null, "Login successful");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // wrapper for signup
        public async Task<(bool Success, string Message)> SignUpAsync(string email, string password, string firstName, string lastName)
        {
            try
            {
                var attrs = new Supabase.Gotrue.UserAttributes
                {
                    Data = new Dictionary<string, object>
                    {
                        { "first_name", firstName },
                        { "last_name", lastName }
                    }
                };

                var session = await _client.Auth.SignUp(email, password, new Supabase.Gotrue.SignUpOptions { Data = attrs.Data });
                return (session != null, "Sign up successful! Please check your email.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // wrapper for logout
        public async Task LogoutAsync()
        {
            await _client.Auth.SignOut();
        }

        // --- Inventory CRUD ---

        public async Task<List<Models.FoodItem>> GetInventoryAsync()
        {
            try
            {
                var response = await _client.From<Models.FoodItem>().Get();
                return response.Models;
            }
            catch (Exception)
            {
                return new List<Models.FoodItem>();
            }
        }

        public async Task<bool> AddFoodItemAsync(Models.FoodItem item)
        {
            try
            {
                var response = await _client.From<Models.FoodItem>().Insert(item);
                return response.Models.Count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateFoodItemAsync(Models.FoodItem item)
        {
            try
            {
                var response = await _client.From<Models.FoodItem>().Update(item);
                return response.Models.Count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteFoodItemAsync(Models.FoodItem item)
        {
            try
            {
                await _client.From<Models.FoodItem>().Delete(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
