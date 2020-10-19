package ca.ironstats.recyclerviewrowselection

import android.content.Context
import android.graphics.Color
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.LinearLayout
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView

class CustomAdapter(private val context: Context, private val list: ArrayList<UserModel>,
                    private val listener: OnItemClickListener
) : RecyclerView.Adapter<RecyclerView.ViewHolder>() {

    private inner class ViewHolder internal constructor(itemView: View) : RecyclerView.ViewHolder(itemView),View.OnClickListener {

        internal var tvLabel: TextView
        internal var tvName: TextView

        init {
            tvLabel = itemView.findViewById(R.id.tv_label) // Initialize your All views prensent in list items
            tvName = itemView.findViewById(R.id.tv_name) // Initialize your All views prensent in list items
            itemView.setOnClickListener(this)
        }

        internal fun bind(position: Int) {
            // This method will be called anytime a list item is created or update its data
            //Do your stuff here
            tvLabel.text = list[position].title
            tvName.text = list[position].name
        }

        override fun onClick(v: View?) {
            val position:Int = adapterPosition
            if(position != RecyclerView.NO_POSITION) {
                listener.onItemClick(position,this@CustomAdapter,itemView)
            }
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RecyclerView.ViewHolder {
        return ViewHolder(LayoutInflater.from(context).inflate(R.layout.item_list, parent, false))
    }

    override fun onBindViewHolder(holder: RecyclerView.ViewHolder, position: Int) {
        if(list[position].isSelected){
            holder.itemView.setBackgroundColor(Color.YELLOW)
            holder.itemView.findViewById<LinearLayout>(R.id.linear_content).setBackgroundColor(Color.YELLOW)
        } else{
            holder.itemView.setBackgroundColor(Color.WHITE)
            holder.itemView.findViewById<LinearLayout>(R.id.linear_content).setBackgroundColor(Color.WHITE)
        }
        (holder as ViewHolder).bind(position)
    }

    override fun getItemCount(): Int {
        return list.size
    }

    interface OnItemClickListener{
        fun onItemClick(position: Int,adapter:CustomAdapter,v:View)
    }
}