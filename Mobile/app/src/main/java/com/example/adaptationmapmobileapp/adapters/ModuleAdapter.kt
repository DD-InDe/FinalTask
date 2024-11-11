package com.example.adaptationmapmobileapp.adapters

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.adaptationmapmobileapp.R
import com.example.adaptationmapmobileapp.models.Module

class ModuleAdapter(
    val modules: List<Module>,
    val context: Context
) :
    RecyclerView.Adapter<ModuleAdapter.MyViewHolder>() {
    class MyViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        val nameTV: TextView = view.findViewById(R.id.module_name_tv)
        val deadlineTV: TextView = view.findViewById(R.id.module_deadline_tv)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.module_item, parent, false)
        return MyViewHolder(view)
    }

    override fun getItemCount(): Int {
        return modules.size
    }

    override fun onBindViewHolder(holder: MyViewHolder, position: Int) {
        holder.nameTV.text = "Название:" + modules[position].name
        holder.deadlineTV.text =
            "Срок выполнения:" + modules[position].moduleCompleteDeadline.toString()
    }
}