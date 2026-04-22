using HarmonyLib;
using Il2Cpp;
using MelonLoader;

[assembly: MelonInfo(typeof(PersistentID.PersistentID), "Persistent IDs", "1.0.0", "404NyanFound")]
namespace PersistentID;

public class PersistentID : MelonMod
{
    private static readonly HashSet<int> PatchedDevices = new();

    private const string SwitchPrefix = "404ID:Switch:";
    private const string PatchPanelPrefix = "404ID:PatchPanel:";
    private const string ServerPrefix = "404ID:Server:";

    public override void OnInitializeMelon() => MelonLogger.Msg(System.ConsoleColor.Green, "[Persistent IDs] Hello Administrator!");

    private static string CleanId(string prefix, string switchId, int hashCode)
    {
        if (switchId.StartsWith(prefix) && switchId.Contains('_'))
        {
            string cleanId = switchId.Split('_')[0];

            if (!PatchedDevices.Contains(hashCode))
                MelonLogger.Msg($"Unity Id Removal | Cleaned Device Id | {switchId} -> {cleanId}");

            return cleanId;
        }

        return switchId;
    }

    #region SWITCH ID PERSISTENCE

    [HarmonyPatch(typeof(NetworkSwitch), nameof(NetworkSwitch.Start))]
    public static class NewSwitchIdPatch
    {
        public static void Postfix(NetworkSwitch __instance)
        {
            string currentId = __instance.switchId;

            if (string.IsNullOrEmpty(currentId) || !currentId.StartsWith(SwitchPrefix))
            {
                // Apply new GUID-based ID. Remapping will handle legacy ids.
                string uniqueId = $"{SwitchPrefix}{Guid.NewGuid().ToString()[..8]}";

                // Set values with unique id
                __instance.switchId = uniqueId;
                __instance.gameObject.name = uniqueId;

                // Persist the unique id in the save
                if (SaveSystem.displayToRawMap != null)
                {
                    SaveSystem.displayToRawMap[uniqueId] = uniqueId;

                    if (!string.IsNullOrEmpty(currentId))
                        SaveSystem.displayToRawMap.Remove(currentId); // Clean up the old legacy ids
                }

                // Refresh UI
                __instance.UpdateScreenUI();
            }
        }
    }

    [HarmonyPatch(typeof(NetworkSwitch), nameof(NetworkSwitch.switchId), MethodType.Getter)]
    public static class SwitchIdPropertyPatch
    {
        // Capture generated ID and clean it! SAY NO TO UNITY "GETINSTANCEID" SUFFIXES!
        public static void Postfix(NetworkSwitch __instance, ref string __result)
        {
            if (string.IsNullOrEmpty(__result)) return;

            int instanceKey = __instance.GetHashCode();

            __result = CleanId(SwitchPrefix, __result, instanceKey);

            if (!PatchedDevices.Contains(instanceKey))
                PatchedDevices.Add(instanceKey);
        }
    }

    [HarmonyPatch(typeof(NetworkSwitch), nameof(NetworkSwitch.GenerateUniqueSwitchId))]
    public static class UniqueSwitchIdPatch
    {
        // Capture generated ID and clean it! SAY NO TO UNITY "GETINSTANCEID" SUFFIXES!
        public static void Postfix(NetworkSwitch __instance, ref string __result)
        {
            if (string.IsNullOrEmpty(__result)) return;

            int instanceKey = __instance.GetHashCode();

            __result = CleanId(SwitchPrefix, __result, instanceKey);

            if (!PatchedDevices.Contains(instanceKey))
                PatchedDevices.Add(instanceKey);
        }
    }

    #endregion

    #region PATCH PANEL ID PERSISTENCE

    [HarmonyPatch(typeof(PatchPanel), nameof(PatchPanel.Awake))]
    public static class NewPatchPanelIdpatch
    {
        public static void Postfix(PatchPanel __instance)
        {
            string currentId = __instance.patchPanelId;

            if (string.IsNullOrEmpty(currentId) || !currentId.StartsWith(PatchPanelPrefix))
            {
                // Apply new GUID-based ID. Remapping will handle legacy ids.
                string uniqueId = $"{PatchPanelPrefix}{Guid.NewGuid().ToString()[..8]}";

                // Set values with unique id
                __instance.patchPanelId = uniqueId;
                __instance.gameObject.name = uniqueId;

                // Persist the unique id in the save
                if (SaveSystem.displayToRawMap != null)
                {
                    SaveSystem.displayToRawMap[uniqueId] = uniqueId;

                    if (!string.IsNullOrEmpty(currentId))
                        SaveSystem.displayToRawMap.Remove(currentId); // Clean up the old legacy ids
                }
            }
        }
    }

    [HarmonyPatch(typeof(PatchPanel), nameof(PatchPanel.patchPanelId), MethodType.Getter)]
    public static class PatchPanelIdPropertyPatch
    {
        // Capture generated ID and clean it! SAY NO TO UNITY "GETINSTANCEID" SUFFIXES!
        public static void Postfix(PatchPanel __instance, ref string __result)
        {
            if (string.IsNullOrEmpty(__result)) return;

            int instanceKey = __instance.GetHashCode();

            __result = CleanId(PatchPanelPrefix, __result, instanceKey);

            if (!PatchedDevices.Contains(instanceKey))
                PatchedDevices.Add(instanceKey);
        }
    }

    [HarmonyPatch(typeof(PatchPanel), nameof(PatchPanel.GenerateUniquePatchPanelId))]
    public static class UniquePatchPanelIdPatch
    {
        // Capture generated ID and clean it! SAY NO TO UNITY "GETINSTANCEID" SUFFIXES!
        public static void Postfix(Server __instance, ref string __result)
        {
            if (string.IsNullOrEmpty(__result)) return;

            int instanceKey = __instance.GetHashCode();

            __result = CleanId(PatchPanelPrefix, __result, instanceKey);

            if (!PatchedDevices.Contains(instanceKey))
                PatchedDevices.Add(instanceKey);
        }
    }

    #endregion

    #region SERVER ID PERSISTENCE

    [HarmonyPatch(typeof(Server), nameof(Server.Start))]
    public static class NewServerIdPatch
    {
        public static void Postfix(Server __instance)
        {
            string currentId = __instance.ServerID;

            if (string.IsNullOrEmpty(currentId) || !currentId.StartsWith(ServerPrefix))
            {
                // Apply new GUID-based ID. Remapping will handle legacy ids.
                string uniqueId = $"{ServerPrefix}{Guid.NewGuid().ToString()[..8]}";

                // Set values with unique id
                __instance.ServerID = uniqueId;
                __instance.gameObject.name = uniqueId;

                // Persist the unique id in the save
                if (SaveSystem.displayToRawMap != null)
                {
                    SaveSystem.displayToRawMap[uniqueId] = uniqueId;

                    if (!string.IsNullOrEmpty(currentId))
                        SaveSystem.displayToRawMap.Remove(currentId); // Clean up the old legacy ids
                }

                // Refresh UI
                __instance.UpdateServerScreenUI();
            }
        }
    }

    [HarmonyPatch(typeof(Server), nameof(Server.ServerID), MethodType.Getter)]
    public static class ServerIdPropertyPatch
    {
        // Capture generated ID and clean it! SAY NO TO UNITY "GETINSTANCEID" SUFFIXES!
        public static void Postfix(Server __instance, ref string __result)
        {
            if (string.IsNullOrEmpty(__result)) return;

            int instanceKey = __instance.GetHashCode();

            __result = CleanId(ServerPrefix, __result, instanceKey);

            if (!PatchedDevices.Contains(instanceKey))
                PatchedDevices.Add(instanceKey);
        }
    }

    [HarmonyPatch(typeof(Server), nameof(Server.GenerateUniqueServerId))]
    public static class UniqueServerIdPatch
    {
        // Capture generated ID and clean it! SAY NO TO UNITY "GETINSTANCEID" SUFFIXES!
        public static void Postfix(Server __instance, ref string __result)
        {
            if (string.IsNullOrEmpty(__result)) return;

            int instanceKey = __instance.GetHashCode();

            __result = CleanId(ServerPrefix, __result, instanceKey);

            if (!PatchedDevices.Contains(instanceKey))
                PatchedDevices.Add(instanceKey);
        }
    }

    #endregion

    [HarmonyPatch(typeof(WaypointInitializationSystem), nameof(WaypointInitializationSystem.LoadNetworkState))]
    public static class MapDataHealing
    {
        public static void Prefix(object[] __args)
        {
            if (__args is null || __args.Length < 3) return;
            NetworkSaveData data = (NetworkSaveData)__args[0];

            MelonLogger.Msg("Checking for legacy Switch IDs in map data...");

            foreach (var swData in data.switches)
            {
                string oldId = swData.switchID;
                if (!string.IsNullOrEmpty(oldId) && !oldId.StartsWith(SwitchPrefix))
                {
                    string newGuid = $"{SwitchPrefix}{Guid.NewGuid().ToString()[..8]}";

                    swData.switchID = newGuid;

                    int healedCables = 0;
                    foreach (var cable in data.cables)
                    {
                        if (cable.startPoint.switchID == oldId)
                        {
                            cable.startPoint.switchID = newGuid;
                            healedCables++;
                        }
                        if (cable.endPoint.switchID == oldId)
                        {
                            cable.endPoint.switchID = newGuid;
                            healedCables++;
                        }
                    }

                    MelonLogger.Msg($"Legacy Mapping | Switch: {oldId} -> {newGuid} | Healed Cables: {healedCables}");
                }
            }

            foreach (var ppData in data.patchPanels)
            {
                string oldId = ppData.patchPanelID;
                if (!string.IsNullOrEmpty(oldId) && !oldId.StartsWith(PatchPanelPrefix))
                {
                    string newGuid = $"{PatchPanelPrefix}{Guid.NewGuid().ToString()[..8]}";
                    ppData.patchPanelID = newGuid;
                    int healedCables = 0;
                    foreach (var cable in data.cables)
                    {
                        if (cable.startPoint.switchID.StartsWith(oldId))
                        {
                            cable.startPoint.switchID = cable.startPoint.switchID.Replace(oldId, newGuid);
                            healedCables++;
                        }
                        if (cable.endPoint.switchID.StartsWith(oldId))
                        {
                            cable.endPoint.switchID = cable.endPoint.switchID.Replace(oldId, newGuid);
                            healedCables++;
                        }
                    }
                    MelonLogger.Msg($"Legacy Mapping | Patch Panel: {oldId} -> {newGuid} | Healed Cables: {healedCables}");
                }
            }

            foreach (var serverData in data.servers)
            {
                string oldId = serverData.serverID;
                if (!string.IsNullOrEmpty(oldId) && !oldId.StartsWith(ServerPrefix))
                {
                    string newGuid = $"{ServerPrefix}{Guid.NewGuid().ToString()[..8]}";

                    serverData.serverID = newGuid;

                    int healedCables = 0;
                    foreach (var cable in data.cables)
                    {
                        if (cable.startPoint.serverID == oldId)
                        {
                            cable.startPoint.serverID = newGuid;
                            healedCables++;
                        }
                        if (cable.endPoint.serverID == oldId)
                        {
                            cable.endPoint.serverID = newGuid;
                            healedCables++;
                        }
                    }
                    MelonLogger.Msg($"Legacy Mapping | Server: {oldId} -> {newGuid} | Healed Cables: {healedCables}");
                }
            }
        }

        public static void Postfix(WaypointInitializationSystem __instance) => __instance.RequestRouteEvaluation();
    }
}