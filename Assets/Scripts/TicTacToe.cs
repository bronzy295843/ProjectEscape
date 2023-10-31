using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class TicTacToe : MonoBehaviour
{
    bool checker;
    int win;
    public Text btnText00 = null;
    public Text btnText01 = null;
    public Text btnText02 = null;

    public Text btnText10 = null;
    public Text btnText11 = null;
    public Text btnText12 = null;

    public Text btnText20 = null;
    public Text btnText21 = null;
    public Text btnText22 = null;

    bool isPLayer;

    public Text winMessage = null;

    int checkWin()//player win is 1 player loss is 0
    {
        //horizontal win check
        if (btnText00.text == btnText01.text && btnText01.text == btnText02.text)
        {
            if (isPLayer)
                return 1;
            else
                return 0;

        }
        else if (btnText10.text == btnText11.text && btnText11.text == btnText12.text)
        {
            if (isPLayer)
                return 1;
            else
                return 0;

        }
        else if (btnText20.text == btnText21.text && btnText21.text == btnText22.text)
        {
            if (isPLayer)
                return 1;
            else
                return 0;

        }
        //vertical win check
        else if (btnText00.text == btnText10.text && btnText10.text == btnText20.text)
        {
            if (isPLayer)
                return 1;
            else
                return 0;

        }
        else if (btnText01.text == btnText11.text && btnText11.text == btnText21.text)
        {
            if (isPLayer)
                return 1;
            else
                return 0;

        }
        else if (btnText02.text == btnText12.text && btnText12.text == btnText22.text)
        {
            if (isPLayer)
                return 1;
            else
                return 0;

        }
        //diagonal win check
        else if (btnText00.text == btnText11.text && btnText11.text == btnText22.text)
        {
            if (isPLayer)
                return 1;
            else
                return 0;

        }
        else if (btnText02.text == btnText11.text && btnText11.text == btnText20.text)
        {
            if (isPLayer)
                return 1;
            else
                return 0;

        }
        else
            return 1;
    }

    public void btnPress00()
    {
        if(isPLayer==true && btnText00.text==null)
        {
            btnText00.text = "X";
            isPLayer = false;

        }
        int isWin = checkWin();
        if (isWin == 1) { winMessage.text = "You Win!"; }
        
    }

    public void btnPress01()
    {
        if (isPLayer == true && btnText01.text == null)
        {
            btnText00.text = "X";
            isPLayer = false;

        }
        int isWin = checkWin();
        if (isWin == 1) { winMessage.text = "You Win!"; }
    }
    public void btnPress02()
    {
        if (isPLayer == true && btnText02.text == null)
        {
            btnText00.text = "X";
            isPLayer = false;

        }
        int isWin = checkWin();
        if (isWin == 1) { winMessage.text = "You Win!"; }
    }
    public void btnPress10()
    {
        if (isPLayer == true && btnText10.text == null)
        {
            btnText00.text = "X";
            isPLayer = false;

        }
        int isWin = checkWin();
        if (isWin == 1) { winMessage.text = "You Win!"; }
    }
    public void btnPress11()
    {
        if (isPLayer == true && btnText11.text == null)
        {
            btnText00.text = "X";
            isPLayer = false;

        }
        int isWin = checkWin();
        if (isWin == 1) { winMessage.text = "You Win!"; }
    }
    public void btnPress12()
    {
        if (isPLayer == true && btnText12.text == null)
        {
            btnText00.text = "X";
            isPLayer = false;

        }
        int isWin = checkWin();
        if (isWin == 1) { winMessage.text = "You Win!"; }
    }
    public void btnPress20()
    {
        if (isPLayer == true && btnText20.text == null)
        {
            btnText00.text = "X";
            isPLayer = false;

        }
        int isWin = checkWin();
        if (isWin == 1) { winMessage.text = "You Win!"; }
    }
    public void btnPress21()
    {
        if (isPLayer == true && btnText21.text == null)
        {
            btnText00.text = "X";
            isPLayer = false;

        }
        int isWin = checkWin();
        if (isWin == 1) { winMessage.text = "You Win!"; }
    }
    public void btnPress22()
    {
        if (isPLayer == true && btnText22.text == null)
        {
            btnText00.text = "X";
            isPLayer = false;

        }
        int isWin = checkWin();
        if (isWin == 1) { winMessage.text = "You Win!"; }
    }
    
    void opponentTurn()
    {
        if(isPLayer==false)
        {
            int randomNumber = UnityEngine.Random.Range(0,8);
            if(randomNumber == 0 && btnText00.text != "X")
            {
                btnText00.text = "O";
                

            }
            else if (randomNumber == 1 && btnText01.text != "X")
            {
                btnText00.text = "O";

            }
            else if (randomNumber == 2 && btnText02.text != "X")
            {
                btnText00.text = "O";

            }
            else if (randomNumber == 3 && btnText10.text != "X")
            {
                btnText00.text = "O";

            }
            else if (randomNumber == 4 && btnText11.text != "X")
            {
                btnText00.text = "O";

            }
            else if (randomNumber == 5 && btnText12.text != "X")
            {
                btnText00.text = "O";

            }
            else if (randomNumber == 6 && btnText20.text != "X")
            {
                btnText00.text = "O";

            }
            else if (randomNumber == 7 && btnText21.text != "X")
            {
                btnText00.text = "O";

            }
            else if (randomNumber == 8 && btnText22.text != "X")
            {
                btnText00.text = "O";

            }
            
            int oppWinCheck = checkWin();
            isPLayer = true;
            if (oppWinCheck == 0)
            {
                resetGame();   
            }
        }
    }

    void resetGame()
    {
        btnText00 = null;
        btnText01 = null;
        btnText02 = null;

        btnText10 = null;
        btnText11 = null;
        btnText12 = null;

        btnText20 = null;
        btnText21 = null;
        btnText22 = null;
}

    void playGame()
    {
        if (isPLayer != true)
        {
            opponentTurn();

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        isPLayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        playGame();
        if(winMessage.text=="You Win!")
        {
            SceneManager.LoadScene("ROOM2");
        }
    }
}
