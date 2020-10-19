package ca.ironstats.demonda.ui.home

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class HomeViewModel : ViewModel() {
    private val user = "John Doe"
    private val _text = MutableLiveData<String>().apply {
        value = "Hello $user"
    }
    val text: LiveData<String> = _text
}