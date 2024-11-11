package com.example.adaptationmapmobileapp.models

data class Module(
    val id:Int,
    val name:String,
    val responsiblePerson: Employee,
    val moduleCompleteDeadline:Int
)
