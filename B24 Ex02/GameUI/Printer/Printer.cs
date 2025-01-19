using System;
using System.Text;
using MemoryGame.Board;
using MemoryGame.DTO;

namespace MemoryGame.Printer
{
    internal class OutPutPrinter
    {
        internal static void PrintGameBoard(GameBoard<char> i_GameBoard)
        {
            uint rows = i_GameBoard.NumberOfRows;
            uint columns = i_GameBoard.NumberOfColumns;
            StringBuilder stringBuilder = new StringBuilder();
            SquareCoordinate coordinate;

            stringBuilder.Append($"  ");
            for(char colLabel = 'A'; colLabel < 'A' + columns; colLabel++)
            {
                stringBuilder.Append($"  {colLabel} ");
            }

            stringBuilder.Append($"{Environment.NewLine}");
            for(uint row = 0; row < rows; row++)
            {
                printSeparetionRow(stringBuilder, columns);
                stringBuilder.Append($"={Environment.NewLine}");
                stringBuilder.Append((row + 1).ToString() + " ");
                for(uint column = 0; column < columns; column++)
                {
                    coordinate = new SquareCoordinate(row, column);
                    stringBuilder.Append($"| {i_GameBoard.GetSquareValue(coordinate)} ");
                }

                stringBuilder.Append($"|{Environment.NewLine}");
            }

            printSeparetionRow(stringBuilder, columns);
            stringBuilder.Append($"={Environment.NewLine}");
            Console.Write(stringBuilder.ToString());
        }
  
        private static void printSeparetionRow(StringBuilder stringBuilder, uint columns)
        {
            stringBuilder.Append("  ");
            for(int column = 0; column < columns; column++)
            {
                stringBuilder.Append("====");
            }
        }

        internal static void PrintGameStatistics(GameStatistics i_GameStatistics)
        {
            StringBuilder stringBuilder = new StringBuilder();
            GameInfoPlayer winnerPlayer = i_GameStatistics.WinnerPlayer;

            stringBuilder.AppendLine();
            if(!i_GameStatistics.Draw)
            {
                stringBuilder.AppendLine(MessageHelper.GetMessage(eGameMessages.AskForWinnerInformation, winnerPlayer.Name, winnerPlayer.Score));
            }
            else
            {
                stringBuilder.AppendLine(MessageHelper.GetMessage(eGameMessages.ThereIsNoWinnerText));
            }

            foreach(GameInfoPlayer PlayerInfo in i_GameStatistics.ListOfPlayers)
            {
                stringBuilder.AppendLine(MessageHelper.GetMessage(eGameMessages.PlayerInfoText,PlayerInfo.Name, PlayerInfo.Score));
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        internal static void GoodByeMessage()
        {
            Console.WriteLine("Thank you for playing!, see you next time.");
        }

        internal static void PrintMessage(string i_Message)
        {
            Console.WriteLine($"{i_Message}");
        }

        internal static void PrintError(string i_Error)
        {
            Console.WriteLine($"Error: {i_Error}");
        }

        public enum eGameMessages
        {
            WelcomeMessage,
            GameSetupComplete,
            ContinueOrExit,
            GoodByeMessage,
            InvalidInput,
            AskForPlayerName,
            AskForGameMode,
            AskForBoardDimensions,
            InvalidBoardDimensions,
            InvalidMove,
            PlayerTurn,
            PlayerWins,
            TieGame,
            InvalidSyntacticInput,
            WelcomePlayer,
            InvalidBoardSize,
            InvalidNumber,
            EvenDimensionsRequired,
            EnterRows,
            EnterColumns,
            InvalidInputRange,
            InvalidPlayerName,
            AskForFirstPlayerMove,
            AskForSecondPlayerMove,
            AskForBotMove,
            AlreadyRevealedSquare,
            OverflowingBordersInput,
            InvalidGameModeSelected,
            AskForSecondPlayerName,
            InvalidSelectionInput,
            StatisticsTitleText,
            AskForWinnerInformation,
            ThereIsNoWinnerText,
            PlayerInfoText
        }
        public static class MessageHelper
        {
            public static string GetMessage(eGameMessages message, params object[] args)
            {
                switch (message)
                {
                    case eGameMessages.StatisticsTitleText:
                        return string.Format("The game statistics:{0}", Environment.NewLine);
                    case eGameMessages.AskForWinnerInformation:
                        return string.Format("The winner is: {0}: Score - {1}", args[0], args[1]);
                    case eGameMessages.ThereIsNoWinnerText:
                        return string.Format("There is not winner, the game ended with draw :)!");
                    case eGameMessages.PlayerInfoText:
                        return string.Format("Player name: {0}: Score - {1}", args[0], args[1]);
                    case eGameMessages.WelcomeMessage:
                        return string.Format("Welcome to our memory game :) !");
                    case eGameMessages.GameSetupComplete:
                        return string.Format("Game setup complete. Starting the game...");
                    case eGameMessages.ContinueOrExit:
                        return string.Format("Do you want to continue to another round or exit? {0}Press 1 to continue or any other key to exit.", Environment.NewLine);
                    case eGameMessages.GoodByeMessage:
                        return string.Format("Thank you for playing! Goodbye!");
                    case eGameMessages.InvalidInput:
                        return string.Format("Invalid input. Please try again.");
                    case eGameMessages.AskForPlayerName:
                        return string.Format("Please enter your name:");
                    case eGameMessages.AskForFirstPlayerMove:
                        return string.Format("{0} please choose a first square: ", args[0]);
                    case eGameMessages.AskForSecondPlayerMove:
                        return string.Format("{0} please choose a second square: ", args[0]);
                    case eGameMessages.OverflowingBordersInput:
                        return string.Format("Overflowing borders input, Please enter a valid input: ");
                    case eGameMessages.AskForBotMove:
                        return string.Format("{0}, making his move.", args[0]);
                    case eGameMessages.AskForGameMode:
                        return string.Format("Please select the game mode:{0}1 for Player vs Computer{0}2 for Player vs Player", Environment.NewLine);
                    case eGameMessages.AlreadyRevealedSquare:
                        return string.Format("This square is already revealed, choose another square");
                    case eGameMessages.AskForBoardDimensions:
                        return string.Format("Enter the dimensions for the board (minimum {0}x{0}, maximum {1}x{1}.{2}The number of squares should be even:", args[0], args[1], Environment.NewLine);
                    case eGameMessages.InvalidBoardDimensions:
                        return string.Format("Invalid board dimensions. Please enter dimensions within the range of {0}x{0} to {1}x{1}.", args[0], args[1]);
                    case eGameMessages.InvalidMove:
                        return string.Format("Invalid move. Please try again.");
                    case eGameMessages.PlayerTurn:
                        return string.Format("It's {0}'s turn. Please make your move.", args[0]);
                    case eGameMessages.PlayerWins:
                        return string.Format("{0} wins the game! Congratulations!", args[0]);
                    case eGameMessages.TieGame:
                        return string.Format("The game is a tie! Well played both!");
                    case eGameMessages.InvalidSyntacticInput:
                        return string.Format("Invalid Syntactic input, Please enter a valid input: ");
                    case eGameMessages.WelcomePlayer:
                        return string.Format("Welcome {0}!{1}", args[0], Environment.NewLine);
                    case eGameMessages.InvalidBoardSize:
                        return string.Format("Invalid board size. Please try again.");
                    case eGameMessages.InvalidNumber:
                        return string.Format("Invalid number. Please try again.");
                    case eGameMessages.EvenDimensionsRequired:
                        return string.Format("The dimensions should be even.");
                      case eGameMessages.EnterRows:
                        return string.Format("Enter number of rows: ");
                    case eGameMessages.InvalidGameModeSelected:
                        return string.Format("Invalid game mode selected.");
                    case eGameMessages.AskForSecondPlayerName:
                        return string.Format("Enter the name of the second player:");
                    case eGameMessages.EnterColumns:
                        return string.Format("Enter number of columns: ");
                    case eGameMessages.InvalidInputRange:
                        return string.Format("Invalid input. Please enter a number between {0} and {1}:", args[0], args[1]);
                    case eGameMessages.InvalidSelectionInput:
                        return string.Format("Invalid input. Please enter 1 or 2:");
                    case eGameMessages.InvalidPlayerName:
                        return string.Format("{0} is not a valid name.{1}Enter a new name that not contains Bot and not Empty:", args[0], Environment.NewLine);
                    default:
                        return nameof(message) + message;
                }
            }
        }
    }
}
