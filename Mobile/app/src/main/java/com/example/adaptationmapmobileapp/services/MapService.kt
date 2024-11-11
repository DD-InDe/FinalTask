package com.example.adaptationmapmobileapp.services

import com.example.adaptationmapmobileapp.models.AdaptationMap
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext
import okhttp3.Request

class MapService {
    private val url = ApiHelper.base + "Map"

    suspend fun getMap(employeeId: Int): AdaptationMap =
        withContext(Dispatchers.IO) {
            var map: AdaptationMap = AdaptationMap()

            try {
                val request = Request.Builder().get().url(url + "/${employeeId}").build()
                val response = ApiHelper.client.newCall(request).execute()
                if (response.isSuccessful) {
                    val type = object : TypeToken<AdaptationMap>() {}.type
                    val json = response.body?.string().toString()
                    map = Gson().fromJson(json, type)
                    map
                } else {
                    throw Exception("Ошибка API. Статус: ${response.code}")
                }
            } catch (exception: Exception) {
                throw exception
            }
        }
}