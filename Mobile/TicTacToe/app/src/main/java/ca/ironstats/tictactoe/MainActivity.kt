package ca.ironstats.tictactoe

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.view.View.INVISIBLE
import android.view.View.VISIBLE
import android.widget.Button
import android.widget.TableLayout
import android.widget.TableRow
import android.widget.Toast
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.android.synthetic.main.login_activity.*

private var SELECTED_PLAYER: Int = 0
private var PREVIOUS_WINNER: Int = -1 //Out of range for init

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.login_activity)
    }

    fun startGame(view: View) {
        Player.addPlayer(ttpn_player1.text.toString(), 0, "X")
        Player.addPlayer(ttpn_player2.text.toString(), 0, "O")
        setContentView(R.layout.activity_main)

        //Initialize player values
        val player = Player.Players

        txv_player_one_name.text = player[0].name
        txv_player_one_points.text = player[0].points.toString()

        txv_player_two_name.text = player[1].name
        txv_player_two_points.text = player[1].points.toString()
    }

    fun selectCase(view: View) {
        val btn = view as Button
        val player = Player.Players

        verifyBoardForWin(player[SELECTED_PLAYER], btn)
    }

    private fun alternatePlayers(){
        SELECTED_PLAYER = if (SELECTED_PLAYER == 0) { //Alternate Players
            1
        } else {
            0
        }
    }

    private fun verifyBoardForWin(player: Player, btn: Button) {
        val symbol = player.symbol

        if (btn.text == "+") {
            btn.text = symbol
        } else {
            Toast.makeText(this, "This button has already been selected", Toast.LENGTH_LONG).show()
            return
        }
        when { // Verify a combination of buttons to know if the player won or not
            (btn_tl.text == symbol && btn_tm.text == symbol && btn_tr.text == symbol) -> win(player) //Row 1
            (btn_ml.text == symbol && btn_mm.text == symbol && btn_mr.text == symbol) -> win(player) //Row 2
            (btn_bl.text == symbol && btn_bm.text == symbol && btn_br.text == symbol) -> win(player) //Row 3
            (btn_tl.text == symbol && btn_ml.text == symbol && btn_bl.text == symbol) -> win(player) //Col 1
            (btn_tm.text == symbol && btn_mm.text == symbol && btn_bm.text == symbol) -> win(player) //Col 2
            (btn_tr.text == symbol && btn_mr.text == symbol && btn_br.text == symbol) -> win(player) //Col 3
            (btn_tl.text == symbol && btn_mm.text == symbol && btn_br.text == symbol) -> win(player) //Diag from top
            (btn_bl.text == symbol && btn_mm.text == symbol && btn_tr.text == symbol) -> win(player) //Diag from bottom
            verifyDraw() -> btn_restart.visibility = VISIBLE
            else -> alternatePlayers()
        }
    }

    private fun verifyDraw(): Boolean { // Verify if each button has been clicked
        val layout = findViewById<TableLayout>(R.id.tablelayout)
        var btnCount = 0
        var count = 0

        for (i in 0 until layout.childCount) {
            val child: View = layout.getChildAt(i)
            if (child is TableRow) {
                for (j in 0 until child.childCount) {
                    val btn: View = child.getChildAt(j)
                    if (btn is Button) {
                        btnCount++
                        if (btn.text != "+") {
                            count++
                        } else {
                            continue
                        }
                    }
                }
            }
        }
        return if (count == btnCount) { //Draw
            Toast.makeText(this, "This game is a Draw!", Toast.LENGTH_LONG).show()
            alternatePlayers()
            true
        } else {
            false
        }
    }

    private fun win(player: Player) {
        Toast.makeText(
            this,
            "Congratulation ${player.name}! You won this game :)",
            Toast.LENGTH_LONG
        ).show()
        PREVIOUS_WINNER = SELECTED_PLAYER
        player.points += 1

        btn_restart.visibility = VISIBLE
    }

    fun restartGame(view: View) {
        val player = Player.Players
        txv_player_one_points.text = player[0].points.toString()
        txv_player_two_points.text = player[1].points.toString()

        // Reset button text to original value
        val layout = findViewById<TableLayout>(R.id.tablelayout)
        for (i in 0 until layout.childCount) {
            val child: View = layout.getChildAt(i)
            if (child is TableRow) {
                for (j in 0 until child.childCount) {
                    val btn: View = child.getChildAt(j)
                    if (btn is Button) {
                        btn.text = "+"
                    }
                }
            }
        }
        btn_restart.visibility = INVISIBLE
    }
}

class Player(val name: String, var points: Int, val symbol: String) {
    companion object Factory {
        val Players = mutableListOf<Player>()

        fun addPlayer(name: String, points: Int, symbol: String): Player {
            val player = Player(name, points, symbol)
            Players.add(player)
            return player
        }
    }
}