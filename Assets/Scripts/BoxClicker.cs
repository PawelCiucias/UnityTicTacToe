using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TicTacToe.Constants;
using System;

public class BoxClicker : MonoBehaviour
{
    public int X;
    public int Y;
    public ParticleSystem explosionParticleX;
    public ParticleSystem explosionParticleO;
    public GameObject oPrefab;
    public GameObject xPrefab;


    void Awake(){
        var gs = GameSettings.Instance;
        
        Array.ForEach(xPrefab.GetComponentsInChildren<MeshRenderer>(), m => m.material = gs.PlayerX.SelectedMaterial);
        this.explosionParticleX.GetComponent<ParticleSystemRenderer>().material = gs.PlayerX.SelectedMaterial;

        this.explosionParticleO.GetComponent<ParticleSystemRenderer>().material = gs.PlayerO.SelectedMaterial;
        oPrefab.GetComponent<MeshRenderer>().material = gs.PlayerO.SelectedMaterial;
    }
    private void OnMouseDown()
    {
        var mm = GameObject.FindObjectOfType<MainManager>();
        //if left mouse down
        if (Input.GetMouseButtonDown(0) && !mm.IsAiMove())
        {
            var moved = mm.ExecuteMove((X, Y));

            if (moved == SymbolEnum.X)
            {
                this.explosionParticleX.transform.position = this.gameObject.transform.position;
                this.explosionParticleX.Play();
                Instantiate(xPrefab, this.gameObject.transform.position, xPrefab.transform.rotation);
                StartCoroutine("hideMe");
            }
            else if (moved == SymbolEnum.O)
            {
                this.explosionParticleO.transform.position = this.gameObject.transform.position;
                this.explosionParticleO.Play();
                Instantiate(oPrefab, this.gameObject.transform.position, oPrefab.transform.rotation);
                StartCoroutine("hideMe");
            }

          
            
        }
    }

    public void blowUpSquare(SymbolEnum p)
    {
        if (p == SymbolEnum.X)
        {
            this.explosionParticleX.transform.position = this.gameObject.transform.position;
            this.explosionParticleX.Play();
            Instantiate(xPrefab, this.gameObject.transform.position, xPrefab.transform.rotation);
        }
        else
        {
            this.explosionParticleO.transform.position = this.gameObject.transform.position;
            this.explosionParticleO.Play();
            Instantiate(oPrefab, this.gameObject.transform.position, oPrefab.transform.rotation);
        }

        StartCoroutine("hideMe");
    }

    IEnumerator hideMe()
    {
        yield return new WaitForSeconds(0.25f);
        this.gameObject.SetActive(false);
    }
}
