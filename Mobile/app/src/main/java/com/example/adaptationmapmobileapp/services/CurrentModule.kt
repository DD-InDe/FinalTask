package com.example.adaptationmapmobileapp.services

import com.example.adaptationmapmobileapp.models.MapModule

object CurrentModule {
    private var _module: MapModule? = null

    fun setModule(module: MapModule) {
        _module = module
    }

    fun getModule(): MapModule = _module!!

    fun clearModule() {
        _module = null
    }
}