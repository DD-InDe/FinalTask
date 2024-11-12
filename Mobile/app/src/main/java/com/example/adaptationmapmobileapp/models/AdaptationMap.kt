package com.example.adaptationmapmobileapp.models

data class AdaptationMap(
    val adaptationProgramId: Int = 0,
    val modules: List<MapModule> = emptyList()
)
