package com.example.adaptationmapmobileapp.services

import okhttp3.OkHttpClient

object ApiHelper {
    val client = OkHttpClient()
    const val base = "http://172.31.160.1:5064/api/"
}