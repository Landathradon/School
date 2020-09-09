package ca.ironstats.projecttwo

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Button
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    private val userMap = mapOf(
            1 to "Tommy Friedman",
            2 to "Wendy Bryan",
            3 to "Tara Webster",
            4 to "Adolfo Gill",
            5 to "Alberto Villegas",
            6 to "Ingrid Riggs",
            7 to "Raiden Cabrera",
            8 to "Devan Garrett",
            9 to "Drake Doyle",
            10 to "Ronnie Montgomery",
            11 to "Emilee Allison",
            12 to "Omari Warren",
            13 to "Jennifer Vance",
            14 to "Brennen Boyd",
            15 to "Avery Fletcher",
            16 to "Ibrahim Costa",
    )

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
    }

    fun onClick(view: View) {
        val b = view as Button
        val userID = b.text.toString()

        if(userID == "Clear"){
            txv_name.text = ""
        } else {
            txv_name.text = userMap[userID.toInt()].toString()
        }
    }



}