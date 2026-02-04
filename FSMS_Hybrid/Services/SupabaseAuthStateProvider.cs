using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Supabase.Gotrue;

namespace FSMS_Hybrid.Services
{
    public class SupabaseAuthStateProvider : AuthenticationStateProvider
    {
        private readonly Supabase.Client _client;
        private bool _isInitialized = false;

        public SupabaseAuthStateProvider(Supabase.Client client)
        {
            _client = client;
            _client.Auth.AddStateChangedListener((sender, state) => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!_isInitialized)
            {
                DebugLogger.Log("[SupabaseAuthStateProvider] Initializing Client...");
                await _client.InitializeAsync();
                
                 // Manual recovery fallback if autoloader failed
                if (_client.Auth.CurrentSession == null)
                {
                     DebugLogger.Log("[SupabaseAuthStateProvider] No session after init. Attempting manual restore...");
                     var persistor = new MauiSessionPersistor();
                     var restoredSession = persistor.LoadSession();
                     if (restoredSession != null)
                     {
                         DebugLogger.Log($"[SupabaseAuthStateProvider] Manual restore found session for {restoredSession.User?.Email}. Re-hydrating Auth...");
                         try
                         {
                             if (!string.IsNullOrEmpty(restoredSession.AccessToken) && !string.IsNullOrEmpty(restoredSession.RefreshToken))
                             {
                                 await _client.Auth.SetSession(restoredSession.AccessToken, restoredSession.RefreshToken);
                                 DebugLogger.Log("[SupabaseAuthStateProvider] Manual Re-hydration SUCCESS!");
                             }
                         }
                         catch (Exception ex)
                         {
                             DebugLogger.Log($"[SupabaseAuthStateProvider] Manual Re-hydration FAILED: {ex.Message}");
                         }
                     }
                }

                _isInitialized = true;
                DebugLogger.Log($"[SupabaseAuthStateProvider] Client Initialized. Current Session: {(_client.Auth.CurrentSession != null ? "FOUND" : "NULL")}");
            }

            var session = _client.Auth.CurrentSession;

            if (session == null || session.User == null)
            {
                DebugLogger.Log("[SupabaseAuthStateProvider] No active session found. returning Empty State.");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            DebugLogger.Log($"[SupabaseAuthStateProvider] Session Valid for {session.User.Email}");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, session.User.Email ?? ""),
                new Claim(ClaimTypes.NameIdentifier, session.User.Id ?? ""),
                new Claim(ClaimTypes.Email, session.User.Email ?? "")
            };

            var identity = new ClaimsIdentity(claims, "Supabase");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
    }
}
