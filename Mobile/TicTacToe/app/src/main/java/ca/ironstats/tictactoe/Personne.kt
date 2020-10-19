package ca.ironstats.tictactoe

data class Personne(val id: Int, val nom: String, var points: Int, var isSelected: Boolean = false, var assignedPlayer: String = "")