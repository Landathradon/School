package ca.ironstats.recyclerviewrowselection

import android.graphics.Color
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.LinearLayout
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity(),CustomAdapter.OnItemClickListener {
    private val data = arrayListOf<UserModel>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val recyclerview = findViewById<RecyclerView>(R.id.recycler_view)

        data.add(UserModel(title = "item 1",name = "name 1"))
        data.add(UserModel(title = "item 2",name = "name 2"))
        data.add(UserModel(title = "item 3",name = "name 3"))
        data.add(UserModel(title = "item 4",name = "name 4"))
        data.add(UserModel(title = "item 5",name = "name 5"))
        data.add(UserModel(title = "item 6",name = "name 6"))
        data.add(UserModel(title = "item 1",name = "name 1"))
        data.add(UserModel(title = "item 2",name = "name 2"))
        data.add(UserModel(title = "item 3",name = "name 3"))
        data.add(UserModel(title = "item 4",name = "name 4"))
        data.add(UserModel(title = "item 5",name = "name 5"))
        data.add(UserModel(title = "item 6",name = "name 6"))


        val adapter = CustomAdapter(this,data,this)
        recyclerview.layoutManager = LinearLayoutManager(this)
        recyclerview.adapter = adapter
        recyclerview.setHasFixedSize(true)

    }


    override fun onItemClick(position: Int,adapter: CustomAdapter,v: View) {
        val clicked_item:UserModel = data[position]
        clicked_item.title = "clicked"
        clicked_item.isSelected = !clicked_item.isSelected
        if(clicked_item.isSelected){
            recycler_view.getChildAt(recycler_view.indexOfChild(v)).setBackgroundColor(Color.YELLOW)
            recycler_view.getChildAt(recycler_view.indexOfChild(v)).findViewById<LinearLayout>(R.id.linear_content).setBackgroundColor(Color.YELLOW)
        }else{
            recycler_view.getChildAt(recycler_view.indexOfChild(v)).setBackgroundColor(Color.WHITE)
            recycler_view.getChildAt(recycler_view.indexOfChild(v)).findViewById<LinearLayout>(R.id.linear_content).setBackgroundColor(Color.WHITE)
        }

    }

}