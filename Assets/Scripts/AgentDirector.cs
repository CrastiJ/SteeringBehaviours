using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GGL;

public class AgentDirector : MonoBehaviour
{
    public Transform selectedTarget;
    public float rayDistance = 1000f;
    public LayerMask selectionLayer;

    private Vector3 selectedTargetPos;

    // Update is called once per frame
    void LateUpdate()
    {
        CheckSelection();
    }

    void CheckSelection()
    {
        //Create a ray from mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Variable to store the hit
        RaycastHit hit;
        //Perform raycast
        if(Physics.Raycast(ray, out hit, rayDistance, selectionLayer))
        {
            GizmosGL.AddSphere(hit.point, 5f);
            if (Input.GetMouseButtonDown(0))
            {
                //Get selected target
                selectedTarget = hit.collider.transform;
                selectedTargetPos = hit.point;
                ApplySelection();
            }
        }
    }

    void ApplySelection()
    {
        AIAgent[] aiAgents = FindObjectsOfType<AIAgent>();
        for (int i = 0; i < aiAgents.Length; i++)
        {
            AIAgent agent = aiAgents[i];
            //Find seek behaviour attached to object
            Seek seek = agent.GetComponent<Seek>();
            SeekPosition seekPos = agent.GetComponent<SeekPosition>();
            //Find flee behaviour attached
            FleePosition fleePos = agent.GetComponent<FleePosition>();
            //if there is a seek behaviour attached
            if(seek != null)
            {
                //Set its target to selected target
                seek.target = selectedTarget;
            }
            //if there is a seek position behaviour attached
            if (seekPos != null)
                seekPos.target = selectedTargetPos;

            if (fleePos != null)
                fleePos.target = selectedTargetPos;
        }
    }
}
