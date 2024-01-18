using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    private static Controls _controls;
    public static void InitM(Player _player1, Player _player2)
    {
        _controls = new Controls();
        PlayerAssigment PlayerAssigment1 = GameObject.Find("PlayerAssigment").GetComponent<PlayerAssigment>();
        _player1 = PlayerAssigment1._players[0];
        _player2 = PlayerAssigment1._players[1];

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

    public static void InitS(Player _player1)
    {
        PlayerAssigment PlayerAssigment1 = GameObject.Find("Player Controller").GetComponent<PlayerAssigment>();
        _player1 = PlayerAssigment1._players[0];
        

        //setting up Player 1's movement 
        _controls.InGame.Player1Movement.performed += _ =>
        {

            _player1.SetMoveDirection(_.ReadValue<Vector2>());
        };

    }

    //Activating the Controls
    public static void EnableInGame()
    {
        _controls.InGame.Enable();
    }
}
