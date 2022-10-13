using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxClicker : MonoBehaviour
{
    public int X;
    public int Y;
    // Start is called before the first frame update
    public ParticleSystem explosionParticleX;
    public ParticleSystem explosionParticleO;
    public Color playerColor;

    private void OnMouseDown()
    {
        Debug.Log("onMouseDown");
        var mm = MainManager.Instance;
        //if left mouse down
        if (Input.GetMouseButtonDown(0) && !mm.IsAiMove() && mm.ExecuteMove(X, Y))
        {
            if (this.explosionParticleX != null)
            {
                if (mm.getPieceToMove() == TicTacToeGrid.Piece.O)
                {
                    var m = mm.PlayerX.SelectedMaterial;
                    this.explosionParticleX.GetComponent<ParticleSystemRenderer>().material = m;
                    this.explosionParticleX.transform.position = this.gameObject.transform.position;
                    this.explosionParticleX.Play();
                }
                else
                {
                    var m = mm.PlayerO.SelectedMaterial;
                    this.explosionParticleO.GetComponent<ParticleSystemRenderer>().material = m;
                    this.explosionParticleO.transform.position = this.gameObject.transform.position;
                    this.explosionParticleO.Play();
                }
            }

            StartCoroutine("hideMe");
        }
    }

    public void blowUpSquare(TicTacToeGrid.Piece p)
    {
        if (p == TicTacToeGrid.Piece.X)
        {
            var m = MainManager.Instance.PlayerX.SelectedMaterial;
            this.explosionParticleX.GetComponent<ParticleSystemRenderer>().material = m;
            this.explosionParticleX.transform.position = this.gameObject.transform.position;
            this.explosionParticleX.Play();
        }
        else
        {
            var m = MainManager.Instance.PlayerO.SelectedMaterial;
            this.explosionParticleO.GetComponent<ParticleSystemRenderer>().material = m;
            this.explosionParticleO.transform.position = this.gameObject.transform.position;
            this.explosionParticleO.Play();
        }

        StartCoroutine("hideMe");
    }

    IEnumerator hideMe()
    {
        yield return new WaitForSeconds(0.25f);
        this.gameObject.SetActive(false);
    }
}
