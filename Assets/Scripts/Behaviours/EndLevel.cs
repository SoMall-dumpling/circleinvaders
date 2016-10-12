using UnityEngine;

public class EndLevel : MonoBehaviour
{

    void Start()
    {
        SingletonMapper.Get<EventManager>().Lose += OnLose;
    }

    void OnLose()
    {
        // TODO destroy player & animate
        // TODO destroy planet & animate
        // TODO remove covers

        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }

}
