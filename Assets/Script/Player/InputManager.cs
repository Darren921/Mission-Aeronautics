using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    private static Controls _controls;
    private static Tutorial tut;
    /*
     public static void InitM(Player _player1, Player _player2)
     {
         _controls = new Controls();
         PlayerAssigment PlayerAssigmentM = GameObject.Find("PlayerAssigment").GetComponent<PlayerAssigment>();
         _player1 = PlayerAssigmentM._players[0];
         _player2 = PlayerAssigmentM._players[1];

         //setting up Player 1's movement 
         _controls.InGame.Player1Movement.performed += _ =>
         {
             _player1.SetMoveDirection(_.ReadValue<Vector2>());
         };
         //setting up Player 2's movement 

         _controls.InGame.Player2Movement.performed += _ =>
         {
             _player2.SetMoveDirection(_.ReadValue<Vector2>());
         };


     }
    */

    public static void InitS(Player _player1)
    {
        _controls = new Controls();
        PlayerAssigment PlayerAssigmentS =  FindObjectOfType<PlayerAssigment>();
        _player1 = PlayerAssigmentS._players[0];
        
        
        //setting up Player 1's movement 
        _controls.InGame.Player1Movement.performed += _ =>
        {
            _player1.SetMoveDirection(_.ReadValue<Vector2>());
        };
        _controls.InGame.Player1NormalAttack.performed += _ =>
        {
            _player1.Attacking();
        };
        _controls.InGame.Player1SpecialAttack.performed += _ =>
        {
            _player1.SpecialAttack();
        };

        _controls.InGame.Player1CollectPowerUp.performed += _ =>
        {
           _player1.CollectPowerUp();

        };
        /*
        _controls.InGame.PlayerShoot.performed += _ =>
        {
            _player1.Shoot(1);
        };
        */
        
        _controls.InGame.PlayerBlock.performed += _ =>
        {
            _player1.Blocking();

        };
        _controls.InGame.PlayerBlock.canceled += _ =>
        {
            _player1.BlockCanceled();

        };




    }

    public static void InitTut(Player _player1)
    {
        _controls = new Controls();
      tut = FindObjectOfType<Tutorial>();   
    PlayerAssigment PlayerAssigmentS = FindObjectOfType<PlayerAssigment>();
        _player1 = PlayerAssigmentS._players[0];
      
            if(Tutorial.movement == true)
             {
            _controls.InGame.Player1Movement.performed += _ =>
            {
                _player1.SetMoveDirection(_.ReadValue<Vector2>());
            };

            if(Tutorial.basicAtk == true)
            {
                _controls.InGame.Player1NormalAttack.performed += _ =>
                {
                    _player1.Attacking();
                };
            }
            if(Tutorial.specialAtk == true) 
            {
                _controls.InGame.Player1SpecialAttack.performed += _ =>
                {
                    _player1.SpecialAttack();
                };
            }
            if(Tutorial.block == true)
            {
                _controls.InGame.PlayerBlock.performed += _ =>
                {
                    _player1.Blocking();

                };
                _controls.InGame.PlayerBlock.canceled += _ =>
                {
                    _player1.BlockCanceled();

                };
            }

            if(Tutorial.powerUps == true)
            {
                _controls.InGame.Player1CollectPowerUp.performed += _ =>
                {
                    _player1.CollectPowerUp();

                };
            }
        }
           

       

    }
        //Activating the Controls
        public static void EnableInGame()
    {


        _controls.InGame.Enable();

    }
    public static void DisableInGame()
    {

        _controls.InGame.Disable();
    }

}
