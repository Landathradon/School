package ca.ironstats.smssender

import android.content.pm.PackageManager
import android.os.Bundle
import android.telephony.PhoneNumberUtils
import android.telephony.SmsManager
import android.view.View.OnFocusChangeListener
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import kotlinx.android.synthetic.main.activity_main.*

private const val REQUEST_PERMISSION_READ_SMS = 1

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        txv_phone.onFocusChangeListener = OnFocusChangeListener { _, hasFocus ->
            if (!hasFocus) {
                val phoneNumber = txv_phone.text.toString()
                if(phoneNumber.isNotEmpty()){
                    val formattedNumber: String = PhoneNumberUtils.formatNumber(phoneNumber, "CA")
                    txv_phone.setText(formattedNumber)
                }
            }
        }
    }

    fun sendSms() {
        val permission: Boolean = checkPerms()

        if (permission) {
            val message = ttml_message.text.toString()
            val phoneNumber = PhoneNumberUtils.normalizeNumber(txv_phone.text.toString())

            try {
                val sms = SmsManager.getDefault()
                sms.sendTextMessage(phoneNumber, null, message, null, null)

                Toast.makeText(applicationContext, "Message sent!", Toast.LENGTH_LONG).show();
            } catch (e: Exception) {
                Toast.makeText(applicationContext, "The message could not send.", Toast.LENGTH_LONG)
                    .show()
                e.printStackTrace()
            } finally {
                ttml_message.setText("")
            }
        }
    }

    private fun checkPerms(): Boolean {

        if (ContextCompat.checkSelfPermission(this, "android.permission.SEND_SMS")
            != PackageManager.PERMISSION_GRANTED) {
            //If permission is not granted, ask for it
            if (ActivityCompat.shouldShowRequestPermissionRationale(
                    this,
                    "android.permission.SEND_SMS"
                )) {

                // Show an explanation to the user *asynchronously* -- don't block
                // this thread waiting for the user's response! After the user
                // sees the explanation, try again to request the permission.

            } else {

                ActivityCompat.requestPermissions(
                    this, arrayOf("android.permission.SEND_SMS"), REQUEST_PERMISSION_READ_SMS
                )

            }

        } else {
            return true
        }

        return false
    }
}