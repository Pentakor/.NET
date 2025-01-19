using static MemoryGame.Printer.OutPutPrinter;
using System;
using System.Text;
using MemoryGame.DTO;
using MemoryGame.Engine;
using Ex02.ConsoleUtils;

namespace MemoryGame.UI
{
    public class GameConsoleUI
    {
        private const bool v_ContinueGame = true;
        private const int k_MaximumDimension = 6;
        private const int k_MinimumDimension = 4;
        private const int k_WaitingTime = 2000;
        private MemoryGameEngine<char> m_GameEngine;
        private static readonly char[] sr_Symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private GameSettings<char> m_GameSetting = new GameSettings<char>(sr_Symbols);

        public void StartGame()
        {
            bool isTheGameContinue = v_ContinueGame;

            PrintMessage(MessageHelper.GetMessage(eGameMessages.WelcomeMessage));
            readPlayersNameAndGameMode();
            getBoardDimension();
            m_GameEngine = new MemoryGameEngine<char>(m_GameSetting);
            while(isTheGameContinue)
            {
                PrintMessage(MessageHelper.GetMessage(eGameMessages.GameSetupComplete));
                runGame();
                PrintMessage(MessageHelper.GetMessage(eGameMessages.ContinueOrExit));
                isTheGameContinue = Console.ReadLine().Equals("1");
                if(isTheGameContinue)
                {
                    getBoardDimension();
                    m_GameEngine.reInitializeBoardSettings(m_GameSetting.BoardRows, m_GameSetting.BoardColumns);
                }
            }

            GoodByeMessage();
            System.Threading.Thread.Sleep(k_WaitingTime * 2);
        } 

        private void runGame()
        {
            while(!m_GameEngine.GameBoardJudge.IsEndOfGame())
            {
                Screen.Clear();
                PrintGameBoard(m_GameEngine.Board);
                makeMove();
            }

            PrintGameStatistics(m_GameEngine.GameBoardJudge.GetGameStatistics());
        } 
        
        private void playerSquaresChoose(out SquareCoordinate io_FirstCoordinate, out SquareCoordinate io_SecondCoordinate)
        {
            PrintMessage(MessageHelper.GetMessage(eGameMessages.AskForFirstPlayerMove, m_GameEngine.GameBoardJudge.CurrentPlayer.Name));
            playerPartialMove(out io_FirstCoordinate);
            PrintMessage(MessageHelper.GetMessage(eGameMessages.AskForSecondPlayerMove, m_GameEngine.GameBoardJudge.CurrentPlayer.Name));
            playerPartialMove(out io_SecondCoordinate);
        }

        private void playerPartialMove(out SquareCoordinate o_PartialMoveCoordinate)
        {
            o_PartialMoveCoordinate = chooseSquare();
            PrintGameBoard(m_GameEngine.Board);
        }

        private bool isPairEqual(SquareCoordinate i_FirstCoordinate, SquareCoordinate i_SecondCoordinate)
        {
            return m_GameEngine.GameBoardJudge.IsPairOfSquaresValuesAreEqual(i_FirstCoordinate, i_SecondCoordinate);
        }

        private void finishTurn(SquareCoordinate i_FirstCoordinateToHide, SquareCoordinate i_SecondCoordinateToHide)
        {
            System.Threading.Thread.Sleep(k_WaitingTime);
            m_GameEngine.HidePairOfSquares(i_FirstCoordinateToHide, i_SecondCoordinateToHide);
            Screen.Clear();
            PrintGameBoard(m_GameEngine.Board);
        }

        private void botSquaresChoose(out SquareCoordinate io_FirstBotCoordinate, out SquareCoordinate o_SecondBotCoordinate)
        {

            io_FirstBotCoordinate = m_GameEngine.GameBotUtillizer.makeRandomMove();
            m_GameEngine.Board.RevealSquare(io_FirstBotCoordinate);
            clearPrintSleepBoard();
            o_SecondBotCoordinate = m_GameEngine.GameBotUtillizer.makeRandomMove(io_FirstBotCoordinate);
            m_GameEngine.Board.RevealSquare(o_SecondBotCoordinate);
            clearPrintSleepBoard();
        }

        private void clearPrintSleepBoard()
        {
            Screen.Clear();
            PrintGameBoard(m_GameEngine.Board);
            PrintMessage(MessageHelper.GetMessage(eGameMessages.AskForBotMove, m_GameEngine.GameBoardJudge.CurrentPlayer.Name));
            System.Threading.Thread.Sleep(k_WaitingTime);
        }

        private void playerDependentMove()
        {
            playerSquaresChoose(out SquareCoordinate firstCoordinate, out SquareCoordinate secondCoordinate);
            validateIfPairEqualOrFinishTurn(firstCoordinate, secondCoordinate); 
        }
        private void validateIfPairEqualOrFinishTurn(SquareCoordinate i_FirstCoordinate, SquareCoordinate i_SecondCoordinate)
        {
            if (isPairEqual(i_FirstCoordinate, i_SecondCoordinate))
            {
                m_GameEngine.GameBoardJudge.UpdateCurrentPlayerScore();
                m_GameEngine.GameBotUtillizer.RemoveSquareFromHiddenList(i_FirstCoordinate);
                m_GameEngine.GameBotUtillizer.RemoveSquareFromHiddenList(i_SecondCoordinate);
            }
            else
            {
                finishTurn(i_FirstCoordinate, i_SecondCoordinate);
                m_GameEngine.GameBoardJudge.AdvanceTurnToNextPlayer();
            }
        }

        private void botMove()
        {
            PrintMessage(MessageHelper.GetMessage(eGameMessages.AskForBotMove, m_GameEngine.GameBoardJudge.CurrentPlayer.Name));
            botSquaresChoose(out SquareCoordinate firstBotCoordinate, out SquareCoordinate secondBotCoordinate);
            validateIfPairEqualOrFinishTurn(firstBotCoordinate, secondBotCoordinate);
        }

        private void makeMove()
        {
            if(!m_GameEngine.GameBoardJudge.CurrentPlayer.Bot)
            {
                playerDependentMove();
            }
            else
            {
                botMove();
            }                 
        }

        private SquareCoordinate chooseSquare()
        {
            bool firstReveal;
            SquareCoordinate coordinate = new SquareCoordinate();

            getSquareCoordinatesFromUser(coordinate);
            firstReveal = m_GameEngine.Board.RevealSquare(coordinate); 
            while(!firstReveal)
            {
                PrintMessage(MessageHelper.GetMessage(eGameMessages.AlreadyRevealedSquare));
                getSquareCoordinatesFromUser(coordinate); 
                firstReveal = m_GameEngine.Board.RevealSquare(coordinate);
            }

            Screen.Clear();

            return coordinate;
        }

        private void getSquareCoordinatesFromUser(SquareCoordinate io_Coordinate)
        {
            getValidSquareCoordinateInput(io_Coordinate);
        }

        private void getValidSquareCoordinateInput(SquareCoordinate io_Coordinate)
        {
            string coordinate = getSyntacticlyValidInput();

            io_Coordinate.Column = (uint)(coordinate[0] - 'A');
            io_Coordinate.Row = ((uint)(coordinate[1] - '0') - 1);
            while(io_Coordinate.Row >= m_GameSetting.BoardRows || io_Coordinate.Column >= m_GameSetting.BoardColumns)
            {
                PrintMessage(MessageHelper.GetMessage(eGameMessages.OverflowingBordersInput));
                coordinate = getSyntacticlyValidInput();
                io_Coordinate.Column = (uint)(coordinate[0] - 'A');
                io_Coordinate.Row = ((uint)(coordinate[1] - '0') - 1);
            }
        }

        private string readInputFromUserOrQuitByChoose()
        {
            string input = Console.ReadLine();

            while(string.IsNullOrEmpty(input))
            {
                input = Console.ReadLine();
            }

            if(char.ToLower(input[0]).Equals('q'))
            {
                Screen.Clear();
                GoodByeMessage();
                System.Threading.Thread.Sleep(k_WaitingTime * 2);
                Environment.Exit(exitCode: 0);
            }

            return input;
        }

        private string getSyntacticlyValidInput()
        {
            string coordinate = readInputFromUserOrQuitByChoose();

            while(coordinate.Length != 2 || !char.IsUpper(coordinate[0]) || (coordinate[1] == '0') || !char.IsDigit(coordinate[1]))
            {
                PrintMessage(MessageHelper.GetMessage(eGameMessages.InvalidSyntacticInput));
                coordinate = readInputFromUserOrQuitByChoose();
            }

            return coordinate;
        }
        
        private void readPlayersNameAndGameMode()
        {
            eGameMode gameMode;

            m_GameSetting.AddPlayerToListOfPlayers(readPlayerName(MessageHelper.GetMessage(eGameMessages.AskForPlayerName)));
            PrintMessage(MessageHelper.GetMessage(eGameMessages.WelcomePlayer, m_GameSetting.GetFirstPlayerName()));
            gameMode = readNumberOfPlayers();

            switch (gameMode)
            {
                case eGameMode.ComputerVsPlayer:
                    m_GameSetting.AddBotToListOfPlayers();
                    break;

                case eGameMode.PlayerVsPlayer:
                    m_GameSetting.AddPlayerToListOfPlayers(readPlayerName(MessageHelper.GetMessage(eGameMessages.AskForSecondPlayerName)));
                    break;

                default:
                    PrintError(MessageHelper.GetMessage(eGameMessages.InvalidGameModeSelected));
                    break;
            }
        }

        private static eGameMode readNumberOfPlayers()
        {
            StringBuilder stringbuilder = new StringBuilder();
            int selectedOption;

            stringbuilder.Append(MessageHelper.GetMessage(eGameMessages.AskForGameMode));
            PrintMessage(stringbuilder.ToString());
            while(!int.TryParse(Console.ReadLine(), out selectedOption) || (selectedOption != (int)eGameMode.ComputerVsPlayer && selectedOption != (int)eGameMode.PlayerVsPlayer))
            {
                PrintMessage(MessageHelper.GetMessage(eGameMessages.InvalidSelectionInput));
            }

            return (eGameMode)selectedOption;
        }

        private string readPlayerName(string i_Prompt)
        {
            PrintMessage(i_Prompt);
            string playerName = Console.ReadLine();

            while(playerName.Contains("Bot_") || string.IsNullOrEmpty(playerName))
            {
                PrintError(MessageHelper.GetMessage(eGameMessages.InvalidPlayerName, playerName));
                playerName = Console.ReadLine();
            }

            return playerName;
        }

        private void getBoardDimension()
        {
            PrintMessage(MessageHelper.GetMessage(eGameMessages.AskForBoardDimensions, k_MinimumDimension, k_MaximumDimension));
            getValidDimension(out uint rows, out uint columns);

            while(!MemoryGameEngine<char>.IsEvenBoardVolume(rows, columns))
            {
                PrintError(MessageHelper.GetMessage(eGameMessages.EvenDimensionsRequired));

                getValidDimension(out rows, out columns);
            }

            m_GameSetting.BoardRows = rows;
            m_GameSetting.BoardColumns = columns;
        }

        private void getValidDimension(out uint o_Rows, out uint o_Columns) 
        {
            PrintMessage(MessageHelper.GetMessage(eGameMessages.EnterRows));
            o_Rows = getValidDimensionInput();
            PrintMessage(MessageHelper.GetMessage(eGameMessages.EnterColumns));
            o_Columns = getValidDimensionInput();
        }

        private uint getValidDimensionInput()
        {
            uint dimension = 0;

            while(!uint.TryParse(Console.ReadLine(), out dimension) || (dimension < k_MinimumDimension || dimension > k_MaximumDimension))
            {
                PrintMessage(MessageHelper.GetMessage(eGameMessages.InvalidBoardDimensions, k_MinimumDimension, k_MaximumDimension));
            }

            return dimension;
        }

        private enum eGameMode
        {
            ComputerVsPlayer = 1,
            PlayerVsPlayer = 2
        }    
    }
}
