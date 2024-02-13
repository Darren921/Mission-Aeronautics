using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class InputManager : MonoBehaviour
{
    private static Controls _controls;
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
        PlayerAssigment PlayerAssigmentS = GameObject.Find("PlayerAssigment").GetComponent<PlayerAssigment>();
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

        _controls.InGame.Player1Boost.performed += _ =>
        {
            _player1.Boost();

        };
        _controls.InGame.PlayerShoot.performed += _ =>
        {
            _player1.Shoot();
        };
    }

    //Activating the Controls
    public static void EnableInGame()
    {
        _controls.InGame.Enable();
    }
}
