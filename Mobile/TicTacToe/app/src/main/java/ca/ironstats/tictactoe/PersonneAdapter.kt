package ca.ironstats.tictactoe

import android.content.Context
import android.graphics.Color
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.LinearLayout
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView

class PersonneAdapter(private val context: Context, private val listePersonnes: ArrayList<Personne>,
                      private val recyclerView: RecyclerView,
                      private val listener: OnItemClickListener
): RecyclerView.Adapter<RecyclerView.ViewHolder>(){

    private inner class ViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView), View.OnClickListener {

        private var tvNom: TextView = itemView.findViewById(R.id.tv_nom)
        private var tvPoints: TextView = itemView.findViewById(R.id.tv_points)

        init {
            itemView.setOnClickListener(this)
        }

        fun bind(position: Int){
            Log.i("XXX", "bind")
            tvNom.text = listePersonnes[position].nom
            tvPoints.text = listePersonnes[position].points.toString()
        }

        override fun onClick(v: View?) {
            val position:Int = adapterPosition
            if(position != RecyclerView.NO_POSITION) {
                listener.onItemClick(position, this@PersonneAdapter, itemView, recyclerView)
            }
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RecyclerView.ViewHolder {
        return ViewHolder(LayoutInflater.from(context).inflate(R.layout.style_dune_ligne, parent, false))
    }

    override fun onBindViewHolder(holder: RecyclerView.ViewHolder, position: Int) {
        val linearContent = holder.itemView.findViewById<LinearLayout>(R.id.linearContent)
        val name = getRVName(recyclerView)

        if(listePersonnes[position].isSelected && listePersonnes[position].assignedPlayer == name){
            linearContent.setBackgroundColor(Color.parseColor("#368BC1"))
        } else{
            linearContent.setBackgroundColor(Color.parseColor("#FFAAAAAA"))
        }

        Log.i("XXX", "onBindViewHolder")
        (holder as ViewHolder).bind(position)
    }

    override fun getItemCount(): Int {
        return listePersonnes.size
    }

    interface OnItemClickListener{
        fun onItemClick(position: Int, adapter: PersonneAdapter, v: View, recyclerView: RecyclerView)
    }

}