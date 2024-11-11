package com.example.adaptationmapmobileapp.services

import com.example.adaptationmapmobileapp.models.Employee
import com.example.adaptationmapmobileapp.models.LoginModel
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.Request
import okhttp3.RequestBody

class AuthorizationService {
    private final val url = ApiHelper.base + "Authorization/"

    suspend fun logIn(model: LoginModel): Employee? =
        withContext(Dispatchers.IO) {
            try {
                var employee: Employee? = null
                val json = Gson().toJson(model)
                val body = RequestBody.create("application/json;charset=utf-8".toMediaType(), json)
                val request = Request.Builder().post(body).url(url + "LogIn").build()
                val response = ApiHelper.client.newCall(request).execute()
                if (response.isSuccessful) {
                    val type = object : TypeToken<Employee>() {}.type
                    employee = Gson().fromJson(response.body?.string().toString(), type)
                }

                employee
            } catch (exception: Exception) {
                throw exception
            }
        }
}