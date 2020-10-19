package ca.ironstats.tictactoe

import android.graphics.Color
import android.os.Bundle
import android.util.Log
import android.view.View
import android.view.View.INVISIBLE
import android.view.View.VISIBLE
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.android.synthetic.main.login_activity.*
import java.lang.Integer.parseInt


private var SELECTED_PLAYER: Int = 0
private var PREVIOUS_WINNER: Int = -1 //Out of range for init
private var ENABLE_DISABLE_BUTTONS: Boolean = true
private var JSON_FILENAME: String = "playerData.json"

class MainActivity : AppCompatActivity(), PersonneAdapter.OnItemClickListener {
    private var listePersonnes: ArrayList<Personne> = ArrayList()
    private lateinit var playerOne: TempPlayer
    private lateinit var playerTwo: TempPlayer
    private lateinit var selectedPlayerImage: ImageView
    private lateinit var rvOne: RecyclerView
    private lateinit var rvTwo: RecyclerView
    private lateinit var pOneImgv: ImageView
    private lateinit var pTwoImgv: ImageView
    private lateinit var pOneName: EditText
    private lateinit var pTwoName: EditText
    private lateinit var pOnePoint: TextView
    private lateinit var pTwoPoint: TextView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.login_activity)

        listePersonnes = gsonStringToArrayList(applicationContext, JSON_FILENAME)

        //Listeners/Adapters
        setupListenersAdapters()

        playerOne = TempPlayer(pOneName.text.toString(), parseInt(pOnePoint.text as String), pOneImgv, R.drawable.ic_x)
        playerTwo = TempPlayer(pTwoName.text.toString(), parseInt(pTwoPoint.text as String), pTwoImgv, R.drawable.ic_o)
    }

    private fun setupListenersAdapters(){
        rvOne = findViewById(rv_player1.id)
        rvTwo = findViewById(rv_player2.id)
        pOneImgv = findViewById(imgv_player1.id)
        pTwoImgv = findViewById(imgv_player2.id)
        pOneName = findViewById(ttpn_player1.id)
        pTwoName = findViewById(ttpn_player2.id)
        pOnePoint = findViewById(tv_player1_points.id)
        pTwoPoint = findViewById(tv_player2_points.id)

        focusChange(pOneName, pOnePoint)
        focusChange(pTwoName, pTwoPoint)
        rvPlayerAdapter(rvOne)
        rvPlayerAdapter(rvTwo)
    }

    private fun rvPlayerAdapter(view: RecyclerView) {
        val adapter = PersonneAdapter(this, listePersonnes, view, this)
        view.layoutManager = LinearLayoutManager(this)
        view.adapter = adapter
        view.setHasFixedSize(true)
    }

    private fun focusChange(view: EditText, playerPoints: TextView) {
        view.onFocusChangeListener = View.OnFocusChangeListener { _, hasFocus ->
            if (!hasFocus) {
                playerPoints.text = "0"
            }
        }
    }

    fun imageClick(view: View) {
        selectedPlayerImage = view as ImageView
        saveLogin()
        setContentView(R.layout.image_selector)
    }

    fun selectImage(view: View){
        val img = view as ImageView
        resumeLogin(view)

        when(selectedPlayerImage.contentDescription.toString()){
            "player1_img" -> {imagesMatch("player1", img)}
            "player2_img" -> {imagesMatch("player2", img)}
        }
    }

    private fun imagesMatch(player: String, img: ImageView){
        val imgTag = getImageByTag(img.contentDescription.toString())

        if(player == "player1" && imgTag != playerTwo.ImageId){
            playerOne.ImageId = imgTag
        } else if(player == "player2" && imgTag != playerOne.ImageId){
            playerTwo.ImageId = imgTag
        } else {
            Toast.makeText(this, "The other player currently has this icon", Toast.LENGTH_LONG).show()
            return
        }

        findViewById<ImageView>(selectedPlayerImage.id).setImageResource(getImageByTag(img.contentDescription.toString()))
    }

    private fun saveLogin(){
        playerOne.name = pOneName.text.toString(); playerOne.points = parseInt(pOnePoint.text as String)
        playerTwo.name = pTwoName.text.toString(); playerTwo.points = parseInt(pTwoPoint.text as String)
    }

    fun resumeLogin(view: View) {
        setContentView(R.layout.login_activity)
        setupListenersAdapters()

        val fakePOne = Personne(1337, playerOne.name, playerOne.points)
        val fakePTwo = Personne(7331, playerTwo.name, playerTwo.points)

        changeNameAndPoints("rv_player1", fakePOne)
        pOneImgv.setImageResource(playerOne.ImageId)

        changeNameAndPoints("rv_player2", fakePTwo)
        pTwoImgv.setImageResource(playerTwo.ImageId)
    }

    fun startGame(view: View) {
        Player.addPlayer(
            playerOne.name,
            playerOne.points,
            ".",
            playerOne.ImageId
        )
        Player.addPlayer(
            playerTwo.name,
            playerTwo.points,
            ",",
            playerTwo.ImageId
        )

        var count = 0
        for(player in Player.Players){
            if(!findPlayerFromJsonData(listePersonnes, player.name)){
                listePersonnes.add(
                    Personne(
                        getLastPlayerID(listePersonnes) + 1,
                        player.name,
                        player.points,
                        false,
                        ""
                    )
                )
                count++
            }
        }

        if(count > 0){
            writeJsonDataToFile(applicationContext, JSON_FILENAME, listePersonnes) //Save new player data
        }

        setContentView(R.layout.activity_main)

        //Initialize player values
        val player = Player.Players

        txv_player_one_name.text = player[0].name
        txv_player_one_points.text = player[0].points.toString()

        txv_player_two_name.text = player[1].name
        txv_player_two_points.text = player[1].points.toString()

        tv_player_turn.text = getString(R.string.turn_player, player[0].name)
    }

    fun selectCase(view: View) {
        val btn = view as Button
        val player = Player.Players

        if (!ENABLE_DISABLE_BUTTONS)
            return

        verifyBoardForWin(player[SELECTED_PLAYER], btn)
    }

    private fun alternatePlayers() {
        SELECTED_PLAYER = if (SELECTED_PLAYER == 0) { //Alternate Players
            1
        } else {
            0
        }
        tv_player_turn.text = getString(R.string.turn_player, Player.Players[SELECTED_PLAYER].name)
    }

    private fun verifyBoardForWin(player: Player, btn: Button) {
        val symbol = player.symbol

        if (btn.text == "+") {
            btn.text = symbol
            btn.setTextColor(Color.parseColor("#FF33B5E5"))
            btn.setCompoundDrawablesWithIntrinsicBounds(0, player.drawable, 0, 0)
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
            ENABLE_DISABLE_BUTTONS = false
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
        ENABLE_DISABLE_BUTTONS = false
        btn_restart.visibility = VISIBLE

        listePersonnes = editPlayerPointsInList(listePersonnes, player)
        writeJsonDataToFile(applicationContext, JSON_FILENAME, listePersonnes) //Save Player Points
    }

    fun restartGame(view: View) {

        ENABLE_DISABLE_BUTTONS = true

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
                        btn.setCompoundDrawablesWithIntrinsicBounds(0, 0, 0, 0)
                        btn.setTextColor(Color.parseColor("#FF000000"))
                        btn.text = "+"
                    }
                }
            }
        }
        btn_restart.visibility = INVISIBLE
    }

    override fun onItemClick(
        position: Int,
        adapter: PersonneAdapter,
        v: View,
        recyclerView: RecyclerView
    ) {
        val selectedPersonne: Personne = listePersonnes[position]
        val name = getRVName(recyclerView)
        //TODO Make a check if user already has selected one Personne
        val userSelectedPersonne = listePersonnes.find { p -> p.assignedPlayer == name }

        if (userSelectedPersonne != null) {
            if(selectedPersonne.isSelected && selectedPersonne.assignedPlayer == name){
                val fakePersonne = Personne(1337, "Player" + name.split("")[name.length], 0)
                selectedPersonne.isSelected = false
                selectedPersonne.assignedPlayer = ""

                selectedPersonneStyle(false, v, recyclerView)
                changeNameAndPoints(name, fakePersonne)
                return
            } else if(userSelectedPersonne.isSelected){
                userSelectedPersonne.isSelected = false
                userSelectedPersonne.assignedPlayer = ""

                val layout = findViewById<RecyclerView>(recyclerView.id)
                for (i in 0 until layout.childCount) {
                    val child: View = layout.getChildAt(i)
                    selectedPersonneStyle(false, child, recyclerView)
                }
            }
        }

        if(selectedPersonne.isSelected && selectedPersonne.assignedPlayer != name ){
            Toast.makeText(this, "This user is already selected", Toast.LENGTH_SHORT).show()
            return
        } else {
            selectedPersonne.isSelected = true
            selectedPersonne.assignedPlayer = name

            selectedPersonneStyle(true, v, recyclerView)

            changeNameAndPoints(name, selectedPersonne)
        }
        saveLogin()
    }

    private fun selectedPersonneStyle(setColor: Boolean, v: View, recyclerView: RecyclerView){
        if(setColor){
            recyclerView.getChildAt(recyclerView.indexOfChild(v)).setBackgroundColor(Color.parseColor("#368BC1"))
            recyclerView.getChildAt(recyclerView.indexOfChild(v)).findViewById<LinearLayout>(R.id.linearContent).setBackgroundColor(Color.parseColor("#368BC1"))
        } else {
            recyclerView.getChildAt(recyclerView.indexOfChild(v)).setBackgroundColor(Color.parseColor("#FFAAAAAA"))
            recyclerView.getChildAt(recyclerView.indexOfChild(v)).findViewById<LinearLayout>(R.id.linearContent).setBackgroundColor(Color.parseColor("#FFAAAAAA"))
        }
    }

    private fun changeNameAndPoints(name: String, personne: Personne){
        Log.i("INFO", "Name: $name | Personne: $personne")
        when(name){
            "rv_player1" -> {pOneName.setText(personne.nom); pOnePoint.text = personne.points.toString()}
            "rv_player2" -> {pTwoName.setText(personne.nom); pTwoPoint.text = personne.points.toString()}
        }
    }
}