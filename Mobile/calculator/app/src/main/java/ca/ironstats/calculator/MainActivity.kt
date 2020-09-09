package ca.ironstats.calculator

import android.os.Bundle
import android.view.View
import android.widget.Button
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        button_clear.setOnClickListener {
            txv_input.text = ""
            txv_result.text = ""
        }
        button_equals.setOnClickListener {
            calculateExpression()
        }
        button_back.setOnClickListener{
            val text = txv_input.text.toString()
            if(text.isNotEmpty()) {
                txv_input.text = text.dropLast(1)
            }

            txv_result.text = ""
        }

    }

    fun onClick(v: View) {
        val b = v as Button
        val buttonText = b.text.toString()

        txv_input.append(buttonText)
        txv_result.text = ""
    }

    private fun calculateExpression() {
        val text = txv_input.text.toString()
        val reg = Regex("(?<=[+\\-*/])|(?=[+\\-*/])")
        val expression = text.split(reg)
        var calc = 0

        // * /, +-
        if (expression.size == 3) {
            for ((index, value) in expression.withIndex()) {
                when (value) {
                    "*" -> calc = multiply(expression, index)
                    "/" -> calc = divide(expression, index)
                    "+" -> calc = add(expression, index)
                    "-" -> calc = subtract(expression, index)
                }
            }
        } else {
            for (x in 1..expression.size step 2) {
                if(x > expression.size - 1){break}
                when (expression[x]) {
                    "*" -> calc = multiply(fixExp(expression, x, calc), x)
                    "/" -> calc = divide(fixExp(expression, x, calc), x)
                    "+" -> calc = add(fixExp(expression, x, calc), x)
                    "-" -> calc = subtract(fixExp(expression, x, calc), x)
                }
            }

        }
        txv_result.text = "Result: $calc"

    }

    private fun add(exp: List<String>, index: Int): Int {
        return (exp[index - 1].toInt() + exp[index + 1].toInt())
    }

    private fun subtract(exp: List<String>, index: Int): Int {
        return (exp[index - 1].toInt() - exp[index + 1].toInt())
    }

    private fun multiply(exp: List<String>, index: Int): Int {
        return (exp[index - 1].toInt() * exp[index + 1].toInt())
    }

    private fun divide(exp: List<String>, index: Int): Int {
        return (exp[index - 1].toInt() / exp[index + 1].toInt())
    }

    private fun fixExp(exp: List<String>, index: Int, calc: Int): List<String> {
        val newExp = exp.toMutableList()
        if(calc > 0){
            newExp[index - 1] = calc.toString()
        }

        return newExp
    }
}