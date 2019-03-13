using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tic_Tac_Whoa
{
// This body holding me,
// Reminds me of my own mortality
// Embrace this moment, remember,
// We are eternal
// All this pain is an illusion

  class Program
  {
    // Will work dynamically with any odd number board size
    // of 3 or greater. Even sizes also work but throws off HAL's
    // intelligence and he starts trying to sing a song.
    
    const int SIZE = 3;
        
    static void Main(string[] args)
    {
      string[,] board = new string[SIZE,SIZE];
      int turnCounter = 0;
      bool gameOver = false;
      bool lessEasy = true;
      string elWin = "There is no winner. Wah wah.";
      bool playerActive = true;
   
      
      CreateBoard(board, ref turnCounter);
      Console.Write("Type ");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.Write("easy");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.Write(" if you want easy mode. Game defaults to less easy mode.\n");
      if (Console.ReadLine() == "easy")
        lessEasy = false;
      
      ShowBoard(board);        
      while (!gameOver)
      {
        Move(ref turnCounter, ref gameOver, ref lessEasy, ref playerActive, board);
        CheckWinner(board, ref elWin, ref gameOver, ref playerActive, ref turnCounter);
        ShowBoard(board);
      }
      ShowBoard(board);
      Console.WriteLine("\n{0}", elWin);
      Console.Read();
    }
    
    static void CreateBoard(string[,] board, ref int turnCounter)
    {
      for (int x = 0; x < SIZE; x++)
      {
        for (int y = 0; y < SIZE; y++)
        {
          turnCounter++;
          board[x,y] = Convert.ToString(turnCounter);
        }
      }
      
    }
    
    
    static void ShowBoard(string[,] board)
    {
      // Going overboard to display the board just because I can
      // Style effects not added into the design, not necessary toward
      // main logic of the game. Also don't want to flow chart out
      // 4 extra for loops.
      
      Console.Clear();
      
      Console.Write("-");
      for (int d = 0; d < SIZE; d++)
        Console.Write("-------");
        
      for (int x = 0; x < SIZE; x++)
      {
        Console.Write("\n|");
        for (int y = 0; y < SIZE; y++)
          Console.Write("      |");

        Console.Write("\n|");
        for (int y = 0; y < SIZE; y++)
        {
          if(board[x,y] == "X")
          {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  {0,2}  ", board[x,y]);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("|");
          }
          else if(board[x,y] == "O")
          {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  {0,2}  ", board[x, y]);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("|");
          }
          else
          {
            Console.Write("  {0,2}  |", board[x,y]);
          }
        }
        
        Console.Write("\n|");
        for (int y = 0; y < SIZE; y++)
          Console.Write("      |");
        
        Console.Write("\n-");
        for (int d = 0; d < SIZE; d++)
          Console.Write("-------");
      }
    }
    
    static void Move(ref int turnCounter, ref bool gameOver, ref bool lessEasy, ref bool playerActive, string[,] board)
    {
      bool validMove = false;
      bool moveMade = false;
      string currentMove = "";
      int input;
      Random randomMove = new Random();
      
      while (!validMove)
      {
        if (playerActive)
        {
          Console.WriteLine("\nPlease enter a valid move.");
          int.TryParse(Console.ReadLine(), out input);
          currentMove = Convert.ToString(input);
        }
        else
        {
          if (lessEasy)
          {
            HAL(board, ref currentMove, ref moveMade);
            Console.WriteLine("\nThe computer has made a move.");
          }
        if (!moveMade)
          currentMove = Convert.ToString(randomMove.Next(1, ((SIZE * SIZE) + 1)));
        }
        for (int x = 0; x < SIZE; x++)
        {
          for (int y = 0; y < SIZE; y++)
          {
            if (board[x,y] == currentMove)
            {
              if (playerActive)
              {
                board[x,y] = "X";
                playerActive = false;
              }
              else
              {
                board[x,y] = "O";
                playerActive = true;
              }
              x = SIZE;
              y = SIZE;
              turnCounter--;
              validMove = true;

            }
          }
        }
      }
    }
    static void CheckWinner(string[,] board, ref string winner, ref bool gameOver, ref bool playerActive, ref int turnCounter)
    {
      bool winCheck = true;
      string testChar = "";
      int currentTest = 0;
      int placeCheck = 0;
      
      while (currentTest < 4)
      {
        if (winCheck)
          testChar = "X";
        else
          testChar = "O";
          
        switch(currentTest)
        {
          case 0:
          {
            for (int y = 0; y < SIZE; y++)
            {
              for (int x = 0; x < SIZE; x++)
              {
                if (board[x,y] == testChar)
                {
                  placeCheck++;
                  if (placeCheck >= SIZE)
                  {
                    if (!playerActive)
                      winner = "The player has won!";
                    else
                      winner = "The computer has won!";
                    
                    gameOver = true;
                    currentTest = 4;
                  }
                }  
                else
                {
                  x = SIZE;
                  placeCheck = 0;
                }
              }
            }
            currentTest++;
            placeCheck = 0;
            break;
          }
          
          case 1:
          {
            for (int x = 0; x < SIZE; x++)
            {
              for (int y = 0; y < SIZE; y++)
              {
                if (board[x, y] == testChar)
                {
                  placeCheck++;
                  if (placeCheck >= SIZE)
                  {
                    if (!playerActive)
                      winner = "The player has won!";
                    else
                      winner = "The computer has won!";

                    gameOver = true;
                    currentTest = 4;
                  }
                }
                else
                {
                  y = SIZE;
                  placeCheck = 0;
                }
              }
            }
            currentTest++;
            placeCheck = 0;
            break;
          }
          
          case 2:
          {
            int dx = 0;
            int dy = 0;
            while (dx < SIZE)
            {
              if (board[dx,dy] == testChar)
              {
                dx++;
                dy++;
                placeCheck++;
              }
              else
              {
                dx = SIZE;
              }
              if (placeCheck >= SIZE)
              {
                if (!playerActive)
                  winner = "The player has won!";
                else
                  winner = "The computer has won!";
                  
                gameOver = true;
                currentTest = 4;
              }
            }
            placeCheck = 0;
            currentTest++;
            break; 
          }
          case 3:
          {
            int dx = 0;
            int dy = SIZE - 1;
            while (dx < SIZE)
            {
              if (board[dx,dy] == testChar)
              {
                dx++;
                dy--;
                placeCheck++;
              }
              else
              {
                dx = SIZE;
                placeCheck = 0;
              }
              if (placeCheck >= SIZE)
              {
                if (!playerActive)
                  winner = "The player has won!";
                else
                  winner = "The computer has won!";
                  
                gameOver = true;
                currentTest = 4;
              }
            }
            placeCheck = 0;
            currentTest++;
            break;
          }
        }
        if (!gameOver && winCheck && currentTest >= 4)
        {
          currentTest = 0;
          winCheck = false;
        }
        else if (gameOver || !winCheck && currentTest >= 4)
          currentTest = 4;
      }
      if (turnCounter <= 0)
        gameOver = true;
    }
    static void HAL(string[,] board, ref string currentMove, ref bool moveMade)
    {
      bool[,] moveBoard = new bool[SIZE,SIZE];
      bool playerCheck = false;
      int placeCounter = 0;
      string searchChar = "O";
      int moveCounter = 0;
      int centerPoint = SIZE / 2;
      
      // Computer knows what the best move on the board is and will default to that
      // if it is available.
      
      if (board[centerPoint,centerPoint] != "X" && board[centerPoint,centerPoint] != "O")
      {
        currentMove = board[centerPoint,centerPoint];
        moveMade = true;
        moveCounter = 4;
      }
      
      while (moveCounter < 4)
      {
        if (playerCheck)
          searchChar = "X";
        else
          searchChar = "O";
        
        switch(moveCounter)
        {
          case 0:
          {
            for (int x = 0; x < SIZE; x++)
            {
              for (int y = 0; y < SIZE; y++)
              {
                if (board[x,y] == searchChar)
                {
                  moveBoard[x,y] = true;
                  placeCounter++;
                }
              }
              if (placeCounter >= SIZE - 1)
              {
                for (int d = 0; d < SIZE; d++)
                {
                  if (!moveBoard[x,d] && board[x,d] != "O" && board[x,d] != "X")
                  {
                    currentMove = board[x,d];
                    moveMade = true;
                    moveCounter = 4;
                    x = SIZE;
                    d = SIZE;
                  }
                }
              }
              else
              {
                placeCounter = 0;
              }
            }
            moveCounter++;
            placeCounter = 0;
            break;
          }

          case 1:
          {
            for (int y = 0; y < SIZE; y++)
            {
              for (int x = 0; x < SIZE; x++)
              {
                if (board[x, y] == searchChar)
                {
                  moveBoard[x, y] = true;
                  placeCounter++;
                }
              }
              if (placeCounter >= SIZE - 1)
              {
                for (int d = 0; d < SIZE; d++)
                {
                  if (!moveBoard[d, y] && board[d, y] != "O" && board[d, y] != "X")
                  {
                    currentMove = board[d, y];
                    moveMade = true;
                    moveCounter = 4;
                    y = SIZE;
                    d = SIZE;
                  }
                }
              }
              else
              {
                placeCounter = 0;
              }
            }
            moveCounter++;
            placeCounter = 0;
            break;
          }

          case 2:
          {
            int dx = 0;
            int dy = 0;
            while (dx < SIZE)
            {
              if (board[dx,dy] == searchChar)
              {
                moveBoard[dx,dy] = true;
                dx++;
                dy++;
                placeCounter++;
              }
              else
              {
                dx++;
                dy++;
              }

              if (placeCounter >= SIZE - 1)
              {
                dx = 0;
                dy = 0;
                while (dx < SIZE)
                {
                  if (!moveBoard[dx, dy] && board[dx, dy] != "O" && board[dx, dy] != "X")
                  {
                    currentMove = board[dx,dy];
                    moveMade = true;
                    dx = SIZE;
                    moveCounter = 4;
                  }
                  dx++;
                  dy++;
                }
              }
              else if (dx == SIZE)
              {
                placeCounter = 0;
              }
              
            }
            moveCounter++;
            placeCounter = 0;
            break;
          }

          case 3:
          {
            int dx = SIZE - 1;
            int dy = 0;
            while (dy < SIZE)
            {
              if (board[dx, dy] == searchChar)
              {
                moveBoard[dx, dy] = true;
                dx--;
                dy++;
                placeCounter++;
              }
              else
              {
                dx--;
                dy++;
              }
              if (placeCounter >= SIZE - 1)
              {
                dx = SIZE - 1;
                dy = 0;
                while (dy < SIZE)
                {
                  if (!moveBoard[dx, dy] && board[dy, dy] != "O" && board[dx, dy] != "X")
                  {
                    currentMove = board[dx, dy];
                    moveMade = true;
                    dy = SIZE;
                    moveCounter = 4;
                  }
                  dx--;
                  dy++;
                }
              }
              else if (dy == SIZE)
              {
                placeCounter = 0;
              }
            }
            moveCounter++;
            placeCounter = 0;
            break;
          }
        }
        
        // Clean up for the next pass
        for (int x = 0; x < SIZE; x++)
        {
          for (int y = 0; y < SIZE; y++)
            moveBoard[x,y] = false;
        }
        
        if (!moveMade && !playerCheck && moveCounter >= 4)
        {
          moveCounter = 0;
          playerCheck = true;
        }
        else if (playerCheck && moveCounter >= 4)
        {
        }
      }
    }
  }
}
