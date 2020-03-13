using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//put this class on the pipelines
public class PipeLineHealth : MonoBehaviour
{
	[SerializeField]
	private float f_minRandomLife = 2f, f_maxRandomLife = 10f;
	private float f_randomLiveTime;
	private Coroutine myCoroutine;

    void Start()
    {
		f_randomLiveTime = Random.Range(f_minRandomLife, f_maxRandomLife);	
	}

	private void OnMouseOver() {
		//use destroyer on rightclick
		if(Input.GetKeyDown(KeyCode.Mouse1)) {
			if(PipeBreaker.instance.UseBreaker()) {
				if(myCoroutine != null) StopCoroutine(myCoroutine);
				StartCoroutine(Break());
			}
		}
	}

	//call this funtion when a pipe is placed
	public void StartBreaking() 
	{
        f_randomLiveTime = Random.Range(f_minRandomLife, f_maxRandomLife);
		myCoroutine = StartCoroutine(BreakDelay());
	}

	//break after a random time
    IEnumerator BreakDelay() 
	{
        //Debug.Log(f_randomLiveTime);
		yield return new WaitForSeconds(f_randomLiveTime);
		StartCoroutine(Break());
	}

	private IEnumerator Break() 
	{
		yield return null;
        transform.GetComponent<PipeLine>().PipeLine_State_To_None();
        //transform.GetComponent<PipeLine>().MyState = PipeLine.PipeLine_State.PS_None;
        //transform.GetComponent<PipeLine>().b_IsPlaced = false;
        PipeLineManager.instance.b_IsPipeLinePlaced
            [(int)transform.GetComponent<PipeLine>().v_MyPosition.x, 
            (int)transform.GetComponent<PipeLine>().v_MyPosition.y,
            (int) transform.GetComponent<PipeLine>().v_MyPosition.z] = false;
        PipesSpawn.instance.ReturnPipeToPool(gameObject);

		transform.GetComponent<PipeInfo>().Breaking();
    }
}
