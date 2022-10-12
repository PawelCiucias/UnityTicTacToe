using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxClicker : MonoBehaviour
{
    public int X;
    public int Y;
    // Start is called before the first frame update
    public ParticleSystem explosionParticle;
    public Color playerColor;

    void Start()
    {
   
    }


    private void OnMouseDown()
    {
        
        //if left mouse down
        if (Input.GetMouseButtonDown(0) && MainManager.Instance.ExecuteMove(X, Y))
        {
            
            if(this.explosionParticle != null){
                
                var m = MainManager.Instance.moveCount % 2 == 1 ? MainManager.Instance.PlayerX.SelectedMaterial : MainManager.Instance.PlayerY.SelectedMaterial;

                this.explosionParticle.GetComponent<ParticleSystemRenderer>().material = m;
                this.explosionParticle.transform.position = this.gameObject.transform.position;
                this.explosionParticle.Play();
            }
            
            StartCoroutine("hideMe");
        }
    }


    IEnumerator hideMe(){
        yield return new WaitForSeconds(0.25f);
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
            //  this.gameObject.transform.up()
    }
}
