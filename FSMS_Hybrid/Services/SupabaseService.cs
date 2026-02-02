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
                AutoConnectRealtime = true
            };

            _client = new Client(Constants.SupabaseUrl, Constants.SupabaseKey, options);
        }

        public async Task InitializeAsync()
        {
            await _client.InitializeAsync();
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
        public async Task<(bool Success, string Message)> SignUpAsync(string email, string password)
        {
            try
            {
                var session = await _client.Auth.SignUp(email, password);
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
    }
}
