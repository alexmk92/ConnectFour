using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class Form1 : Form
    {
        // Determines which row to add the counter on in click method
        int    row        = 0;

        // Determine the stack height for each column
        int    colHeight1 = 0;
        int    colHeight2 = 0;
        int    colHeight3 = 0;
        int    colHeight4 = 0;
        int    colHeight5 = 0;
        int    colHeight6 = 0;
        int    colHeight7 = 0;

        // An empty player counter to be set by method
        Color player1    = Color.Red;
        Color player2    = Color.Blue;

        // Scores
        int   p1Score    = 0;
        int   p2Score    = 0;

        // Create an array which will be used to check for a win
        Color[] win      = new Color[4];

        // Count for how many turns have been taken
        int    turns      = 0;

        // Array to hold the tokens
        Color[,] grid    = new Color[7,6];
        Color[,] empty   = new Color[7,6];

        public Form1()
        {
            InitializeComponent();
            lbl_result.Focus();
            lbl_result.Text = getScore();
        }


        // Click events to handle adding a new counter to the stack
        private void button1_Click(object sender, EventArgs e)
        {
            row = 0;
            Color thisPlayer = getPlayer(turns);

            if (addToStack(row, thisPlayer, colHeight1))
                colHeight1++;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            row = 1;
            Color thisPlayer = getPlayer(turns);

            if (addToStack(row, thisPlayer, colHeight2)) 
                colHeight2++;          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            row = 2;
            Color thisPlayer = getPlayer(turns);

            if (addToStack(row, thisPlayer, colHeight3))
                colHeight3++;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            row = 3;
            Color thisPlayer = getPlayer(turns);

            if (addToStack(row, thisPlayer, colHeight4))
                colHeight4++;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            row = 4;
            Color thisPlayer = getPlayer(turns);

            if (addToStack(row, thisPlayer, colHeight5))
                colHeight5++;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            row = 5;
            Color thisPlayer = getPlayer(turns);

            if (addToStack(row, thisPlayer, colHeight6))
                colHeight6++;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            row = 6;
            Color thisPlayer = getPlayer(turns);

            if (addToStack(row, thisPlayer, colHeight7))
                colHeight7++;
        }

        // Returns either player1 or player2 dependent on a modules operation
        public Color getPlayer(int turns)
        {
            Color thisPlayer = Color.Gray;

            if (turns % 2 == 0)
            {
                thisPlayer = player1;
            }
            else
            {
                thisPlayer = player2;
            }

            return thisPlayer;
        }

        // Returns the current score as a string
        public String getScore()
        {
            String score = "Red: " + p1Score.ToString() + "    Blue: " + p2Score.ToString();
            return score;
        }

        // Add to stack function - adds the new counter to the stack of tokens
        // returns true if the token was added to the stack, else return false
        public Boolean addToStack(int row, Color player, int height)
        {
            Boolean tokenAdded = false;

            // Check that a row and player object have been set
            if (row < 0 && player == null && height < 0)
            {
                tokenAdded = false;
            }
            else
            {
                // Check that the stack height hasnt been reached, if it hasnt then
                // increment the total turns and the players count and update the grid
                if (height < 6)
                {
                    grid[row, height] = player;

                    // Update the GUI to show the player at location
                    updateGrid(grid);

                    // Only check for a win once enough turns have been made (3+)
                    if(turns / 2 > 2)
                        checkResult(grid, player);

                    // Toggle the added flag to true
                    tokenAdded = true;

                    // Increment the amount of global turns
                    turns++;

                }
                else
                {
                    // Specify that the stack is full and return false on the method
                    int thisCol = row + 1;
                    lbl_result.Text = "The stack is full at column " + thisCol + " please try another row.";
                    tokenAdded = false;
                }
            }

            return tokenAdded;
        }

        // Check if anybody has won
        public void checkResult(Color[,] thisGrid, Color player)
        {
            // Check vertical
            if (check_win(thisGrid, player) == true)
            {
                if (player == Color.Red)
                {
                    p1Score++;
                }
                else
                {
                    p2Score++;
                }

                lbl_result.Text = getScore();
                resetGrid(this.grid);
            }
        }

        // Checks the vertical direction
        public Boolean check_win(Color[,] thisGrid, Color player)
        {
            Boolean winner = false;

            // Set the win parameters
            int tokenCount = 0;

            // Check if a vertical sequence appears
            if (winner == false)
            {
                for (int i = 0; i < 6; i++)
                {
                    // reset the token count for each new row
                    tokenCount = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        // Keep checking if the player token passed to the method matches the player found
                        // at the given index, if it does increment a count of consecutive tokens found, if
                        // a count of 4 is reached then break out the inner loop
                        if (thisGrid[i, j] == player)
                        {
                            tokenCount++;
                            lbl_result.Text = getScore();
                            if (tokenCount >= 4)
                            {
                                winner = true;
                                break;
                            }
                               
                        }
                        else
                        {
                            tokenCount = 0;
                            lbl_result.Text = getScore();
                            winner = false;
                        }
                    }
                    if (winner == true)
                        break;
                }
            }


            // Check if a horizontal sequence appears
            if (winner == false)
            {
                for (int i = 0; i < 6; i++)
                {
                    // reset the token count for each new row
                    tokenCount = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        // Keep checking if the player token passed to the method matches the player found
                        // at the given index, if it does increment a count of consecutive tokens found, if
                        // a count of 4 is reached then break out the inner loop
                        if (thisGrid[j, i] == player)
                        {
                            tokenCount++;
                            lbl_result.Text = tokenCount.ToString();
                            if (tokenCount >= 4)
                            {
                                winner = true;
                                break;
                            }

                        }
                        else
                        {
                            tokenCount = 0;
                            lbl_result.Text = tokenCount.ToString();
                            winner = false;
                        }
                    }
                    if (winner == true)
                        break;
                }
            }

            
            return winner;
        }

        // Clears the grid
        public void resetGrid(Color[,] thisGrid)
        {
            // Reset the global variables to allow stack to work
            colHeight1 = 0;
            colHeight2 = 0;
            colHeight3 = 0;
            colHeight4 = 0;
            colHeight5 = 0;
            colHeight6 = 0;
            colHeight7 = 0;

            // Reset to default turns
            turns = 0;

            // Update the score
            lbl_result.Text = getScore();

            // Clear the contents of the grid
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    thisGrid[i, j] = empty[i, j];
                }
            }

            // update the grid
            updateGrid(thisGrid);
            lbl_result.Focus();
        }

        // Update the grid
        public void updateGrid(Color[,] thisGrid)
        {
            // Column 1
            button1.BackColor  = thisGrid[0,5];
            button8.BackColor  = thisGrid[0,4];
            button15.BackColor = thisGrid[0,3];
            button22.BackColor = thisGrid[0,2];
            button29.BackColor = thisGrid[0,1];
            button36.BackColor = thisGrid[0,0];

            // Column 2
            button2.BackColor  = thisGrid[1,5];
            button9.BackColor  = thisGrid[1,4];
            button16.BackColor = thisGrid[1,3];
            button23.BackColor = thisGrid[1,2];
            button30.BackColor = thisGrid[1,1];
            button37.BackColor = thisGrid[1,0];

            // Column 3
            button3.BackColor  = thisGrid[2,5];
            button10.BackColor = thisGrid[2,4];
            button17.BackColor = thisGrid[2,3];
            button24.BackColor = thisGrid[2,2];
            button31.BackColor = thisGrid[2,1];
            button38.BackColor = thisGrid[2,0];

            // Column 4
            button4.BackColor = thisGrid[3,5];
            button11.BackColor = thisGrid[3,4];
            button18.BackColor = thisGrid[3,3];
            button25.BackColor = thisGrid[3,2];
            button32.BackColor = thisGrid[3,1];
            button39.BackColor = thisGrid[3,0];

            // Column 5
            button5.BackColor  = thisGrid[4,5];
            button12.BackColor = thisGrid[4,4];
            button19.BackColor = thisGrid[4,3];
            button26.BackColor = thisGrid[4,2];
            button33.BackColor = thisGrid[4,1];
            button40.BackColor = thisGrid[4,0];

            // Column 6
            button6.BackColor  = thisGrid[5,5];
            button13.BackColor = thisGrid[5,4];
            button20.BackColor = thisGrid[5,3];
            button27.BackColor = thisGrid[5,2];
            button34.BackColor = thisGrid[5,1];
            button41.BackColor = thisGrid[5,0];

            // Column 7
            button7.BackColor  = thisGrid[6,5];
            button14.BackColor = thisGrid[6,4];
            button21.BackColor = thisGrid[6,3];
            button28.BackColor = thisGrid[6,2];
            button35.BackColor = thisGrid[6,1];
            button42.BackColor = thisGrid[6,0];
        }

        // Reset the game
        private void button43_Click(object sender, EventArgs e)
        {
            // Reset the score variables
            p1Score = 0;
            p2Score = 0;

            // Update the grid to a new game
            resetGrid(this.grid);
        }


    }
}
