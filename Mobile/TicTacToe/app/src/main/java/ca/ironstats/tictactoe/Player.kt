package ca.ironstats.tictactoe

import android.widget.ImageView

class Player(val name: String, var points: Int, val symbol: String, val drawable: Int) {
    companion object Factory {
        val Players = mutableListOf<Player>()

        fun addPlayer(name: String, points: Int, symbol: String, drawable: Int): Player {
            val player = Player(name, points, symbol, drawable)
            Players.add(player)
            return player
        }
    }
}

data class TempPlayer(var name: String, var points: Int, val iconIv: ImageView, var ImageId: Int)