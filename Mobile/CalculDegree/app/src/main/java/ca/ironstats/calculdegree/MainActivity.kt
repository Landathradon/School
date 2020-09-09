package ca.ironstats.calculdegree

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
    }

    fun convert(v : View){
        val celsius = etnd_celsius.text.toString().toFloat()
        val fahrenheit = (celsius * 1.8 + 32)
        txv_fahrenheit.text = fahrenheit.toString()
    }

}