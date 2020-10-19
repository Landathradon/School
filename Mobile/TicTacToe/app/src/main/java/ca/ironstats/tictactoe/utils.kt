package ca.ironstats.tictactoe

import android.content.Context
import android.util.Log
import android.view.View
import androidx.recyclerview.widget.RecyclerView
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import java.io.IOException

val gson = Gson()

fun getJsonDataFromFile(context: Context, fileName: String): String? {
    val jsonString: String
    try {
        jsonString = context.openFileInput(fileName).bufferedReader().use { it.readText() }
    } catch (ioException: IOException) {
        Log.i("FILE_ERROR", "File not exist")
//        ioException.printStackTrace()
        return null
    }
    return jsonString
}

fun gsonStringToArrayList(context: Context, fileName: String): ArrayList<Personne>{
    val jsonFileString = getJsonDataFromFile(context, fileName) ?: return ArrayList()
    val listPersonType = object : TypeToken<ArrayList<Personne>>() {}.type
    return gson.fromJson(jsonFileString, listPersonType)
}

fun writeJsonDataToFile(context: Context, fileName: String, list: ArrayList<Personne>){
    for (personne in list){ //Make sure those values are reset before saving
        personne.isSelected = false
        personne.assignedPlayer = ""
    }

    try {
        context.openFileOutput(fileName, Context.MODE_PRIVATE).use {//Create File and write if not exists
            it.write(gson.toJson(list).toString().toByteArray())
        }
    } catch (ioException: IOException) {
        ioException.printStackTrace()
    }
}

fun findPlayerFromJsonData(list: ArrayList<Personne>, playerName: String): Boolean{
    val playerFound: Personne? = list.find { p -> p.nom == playerName }

    return when(playerFound != null){
        true -> {
            true
        }
        false -> {
            false
        }
    }
}

fun getLastPlayerID(list: ArrayList<Personne>): Int {
    return if (list.isEmpty()){
        0
    } else {
        list.last().id
    }
}

fun editPlayerPointsInList(list: ArrayList<Personne>, player: Player): ArrayList<Personne> {
    val playerFound: Personne? = list.find { p -> p.nom == player.name }
    if (playerFound != null) {
        playerFound.points = player.points
    }
    return list
}

fun getImageByTag(tag: String): Int {
    return when(tag){
        "1" -> {R.drawable.ic_x}
        "2" -> {R.drawable.ic_o}
        "3" -> {R.drawable.ic_lightning}
        "4" -> {R.drawable.ic__01_send_button}
        "5" -> {R.drawable.ic__02_black_plane}
        "6" -> {R.drawable.ic__03_instagram_logo}
        "7" -> {R.drawable.ic__04_car_front}
        "8" -> {R.drawable.ic__05_public_transport_subway}
        "9" -> {R.drawable.ic__06_hotel_with_three_floors}
        "10" -> {R.drawable.ic__07_man_cycling}
        "11" -> {R.drawable.ic__08_home_button}
        else -> 0
    }
}

fun getRVName(recyclerView: RecyclerView): String{
    return if (recyclerView.id == View.NO_ID) "" else recyclerView.resources.getResourceName(recyclerView.id).split(":id/")[1]
}