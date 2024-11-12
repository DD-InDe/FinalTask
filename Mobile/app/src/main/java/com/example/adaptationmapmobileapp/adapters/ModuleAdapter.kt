package com.example.adaptationmapmobileapp.adapters

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ProgressBar
import android.widget.RatingBar
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.adaptationmapmobileapp.R
import com.example.adaptationmapmobileapp.models.MapModule
import com.example.adaptationmapmobileapp.models.Module

class ModuleAdapter(
    val modules: List<MapModule>,
    val context: Context
) :
    RecyclerView.Adapter<ModuleAdapter.MyViewHolder>() {
    class MyViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        val nameTV: TextView = view.findViewById(R.id.module_name_tv)
        val deadlineTV: TextView = view.findViewById(R.id.module_deadline_tv)
        val mentorTV: TextView = view.findViewById(R.id.module_mentor_tv)
        val rating: RatingBar = view.findViewById(R.id.module_rating)
        val progress: TextView = view.findViewById(R.id.module_progress)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.module_item, parent, false)
        return MyViewHolder(view)
    }

    override fun getItemCount(): Int {
        return modules.size
    }

    override fun onBindViewHolder(holder: MyViewHolder, position: Int) {
        val module = modules[position]

        holder.nameTV.text = "Название:" + module.module.name
        holder.deadlineTV.text =
            "Срок выполнения:" + module.module.moduleCompleteDeadline.toString()
        holder.mentorTV.text =
            "Наставник: ${module.mentor.lastName} ${module.mentor.firstName}"

        holder.rating.rating = module.rating.toFloat()
    }
}