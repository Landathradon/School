package ca.ironstats.recyclerview

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.android.synthetic.main.style_dune_ligne.view.*

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val p1 = Personne(1,"Martin","Steve")
        val p2 = Personne(2,"Roussel","John")
        val p3 = Personne(3,"Dupont","Eric")
        val personnes = mutableListOf(p1,p2,p3)

        mon_recycler.layoutManager = LinearLayoutManager(this)
        mon_recycler.adapter = PersonneAdapter(personnes.toTypedArray()){
            Toast.makeText(this, "Élément sélectionné: $it",Toast.LENGTH_LONG).show()
        }
    }
}

data class Personne(val id: Long, val nom: String, val prenom: String)

class PersonneAdapter(val personnesAAfficher: Array<Personne>, val listener: (Personne) -> Unit): RecyclerView.Adapter<PersonneAdapter.ViewHolder>(){

    class ViewHolder(private val elementDeListe: View) : RecyclerView.ViewHolder(elementDeListe) {
        fun bind(personne: Personne, listener: (Personne) -> Unit) = with(itemView){
            Log.i("XXX", "bind")
            itemView.tv_nom.text = personne.nom
            itemView.tv_prenom.text = personne.prenom
            setOnClickListener{listener(personne)}
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val uneLigneView = LayoutInflater.from(parent.context).inflate(R.layout.style_dune_ligne, parent, false)
        return ViewHolder(uneLigneView)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        Log.i("XXX", "onBindViewHolder")
        holder.bind(personnesAAfficher[position], listener)
    }

    override fun getItemCount(): Int {
        return personnesAAfficher.size
    }

}