using Supabase.Gotrue;
using Supabase.Gotrue.Interfaces;
using Newtonsoft.Json;

namespace FSMS_Hybrid.Services
{
    public class MauiSessionPersistor : IGotrueSessionPersistence<Session>
    {
        private const string CacheKey = "supabase_session";

        // switching to Preferences to avoid Keychain entitlement issues on unprovisioned Mac builds
        public void SaveSession(Session session)
        {
            DebugLogger.Log($"[MauiSessionPersistor] Saving session (Preferences)... User: {session?.User?.Email}");
            try 
            {
                var json = JsonConvert.SerializeObject(session);
                Preferences.Set(CacheKey, json);
                DebugLogger.Log("[MauiSessionPersistor] Session saved successfully.");
            }
            catch (Exception ex)
            {
                DebugLogger.Log($"[MauiSessionPersistor] Error saving session: {ex.Message}");
            }
        }

        public void DestroySession()
        {
            DebugLogger.Log("[MauiSessionPersistor] Destroying session...");
            Preferences.Remove(CacheKey);
        }

        public Session? LoadSession()
        {
            DebugLogger.Log("[MauiSessionPersistor] Loading session (Preferences)...");
            try
            {
                var json = Preferences.Get(CacheKey, string.Empty);
                
                if (string.IsNullOrEmpty(json)) 
                {
                    DebugLogger.Log("[MauiSessionPersistor] No session found in storage.");
                    return null;
                }
                
                var session = JsonConvert.DeserializeObject<Session>(json);
                DebugLogger.Log($"[MauiSessionPersistor] Session loaded! User: {session?.User?.Email}");
                return session;
            }
            catch (Exception ex)
            {
                DebugLogger.Log($"[MauiSessionPersistor] Error loading session: {ex.Message}");
                return null;
            }
        }
    }
}
