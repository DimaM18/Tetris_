using Client.Scripts.DataStorage;
using Client.Scripts.EmptyGame.LevelObjects;
using Client.Scripts.Tools.Services;
using UnityEngine;

namespace Client.Scripts.EmptyGame.Systems
{
    public class StepTetrominoSystem : IBattleSystem
    {
        public void Start()
        {
        }

        public void Stop()
        {
        }

        public void OnUpdate()
        {           
            if (Data.Game.Empty.Paused.Value)
            {
                return;
            }
            
            ActiveTetromino activeTetromino = Data.SceneLinks.ActiveTetromino;
            Service.BoardService.MapComponent.Clear(activeTetromino);

           if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
            {
                activeTetromino.Rotate(1);
            }
            
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                activeTetromino.HardDrop();
            }

            if (Time.time > activeTetromino.MoveTime) 
            {
                activeTetromino.HandleMoveInputs();
            }

            if (Time.time > activeTetromino.StepTime)
            {
                activeTetromino.Step();
            }
            
            Service.BoardService.MapComponent.Set(activeTetromino);
        }
    }
}