using UnityEngine;

public class DiedState : BaseState
{
    public DiedState(StateMachine stateMachine, BasePersonnage personnage) : base(stateMachine, personnage) { }
    private Transform bloodSpray;
    private float timer = 0f;
    private float sprayDuration = 8f;
    private int sprayIndex = 0;
    public override void Enter()
    {
        _personnage._audioPlayer.PlayDeathSound();
        _personnage.DestroyComponents();
        _personnage.transform.Rotate(-90f, 0f, 0f);
        bloodSpray = _personnage.transform.Find("Rig/B-root/B-hips/B-spine/BloodSprayFX");
        foreach (Transform child in bloodSpray)
        {
            child.GetComponent<ParticleSystem>().Play();
        }
    }

    public override void Exit()
    {
        _personnage._canUpdate = false; //Le state machine va arreter de s'update
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= sprayDuration / 3) //Je divise en 3 car il y'a 3 systemes de particules a arreter dans les 8 secondes. Ils vont s'arreter 1 a la fois
        {
            timer = 0f;
            bloodSpray.GetChild(sprayIndex).GetComponent<ParticleSystem>().Stop();
            sprayIndex++;
        }
        if (sprayIndex >= bloodSpray.childCount)
        {
            Exit();
        }
    }
}
