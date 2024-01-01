using UnityEngine;

namespace NPC
{
    public class NPCPotentialKiller : MonoBehaviour
    {
        public GameObject targetNPC;

        public void KillTargetNPC()
        {
            targetNPC.GetComponent<NPCState>().Die();
            GetComponent<NPCState>().GetSmeared();
        }
    }
}