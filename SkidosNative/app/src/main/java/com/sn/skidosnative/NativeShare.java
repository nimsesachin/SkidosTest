package com.sn.skidosnative;

import android.app.Activity;
import android.net.Uri;
import android.util.Log;
import android.database.Cursor;
import android.content.ContentValues;
import com.google.gson.*;

public class NativeShare {
    protected Activity mCurrentActivity;

    public void setActivity(Activity activity) {
        mCurrentActivity = activity;
    }

    public boolean KeyExists(String key)
    {
        Uri queryString = Uri.withAppendedPath(SkidosContentProvider.CONTENT_URI, key);

        Cursor cursor = mCurrentActivity.getContentResolver().query(queryString, null, null, null);
        return  (cursor != null && cursor.getCount() == 1);
    }

    public boolean SaveData(String key, String value) {

        if (mCurrentActivity == null) {
            Log.e("Native Share", "Android Activity is null");
            return false;
        }

        ContentValues values = new ContentValues();
        values.put("id_key", key);
        values.put("value", value);

        if(KeyExists(key))
        {
            Uri queryString = Uri.withAppendedPath(SkidosContentProvider.CONTENT_URI, key);
            int count = mCurrentActivity.getContentResolver().update(queryString, values, null, null);
            Log.e("Native Share", "Update" + count);
            return count == 1;
        }
        else
            return (mCurrentActivity.getContentResolver().insert(SkidosContentProvider.CONTENT_URI, values) != null);
    }

    public String GetData(String key) {

        if (mCurrentActivity == null) {
            Log.e("Native Share", "Android Activity is null");
            return "";
        }

        Uri queryString = Uri.withAppendedPath(SkidosContentProvider.CONTENT_URI, key);

        Cursor cursor = mCurrentActivity.getContentResolver().query(queryString, null, null, null);
        String value = "";
        if (cursor != null && cursor.moveToFirst()) {
            Log.e("Native Share", "GetData Count:"+cursor.getCount());
            value = cursor.getString(cursor.getColumnIndex("value"));
            cursor.close();
        }
        return value;
    }

    public String GetAllData() {

        if (mCurrentActivity == null) {
            Log.e("Native Share", "Android Activity is null");
            return "";
        }

        Uri queryString =SkidosContentProvider.CONTENT_URI;

        Cursor cursor = mCurrentActivity.getContentResolver().query(queryString, null, null, null);
        String value = "";
        if (cursor != null )
        {
            Log.e("Native Share", "GetAllData Count:"+cursor.getCount());
            Data[] data = new Data[cursor.getCount()];
            int i = 0;
            int keyIndex = cursor.getColumnIndex("id_key");
            int valueIndex = cursor.getColumnIndex("value");
            for (boolean hasItem = cursor.moveToFirst(); hasItem; hasItem = cursor.moveToNext()) {
                data[i] = new Data();
                data[i].key = cursor.getString(keyIndex);
                data[i].value = cursor.getString(valueIndex);
                i++;
            }
            cursor.close();
            value = new Gson().toJson(data);
        }
        return value;
    }
}
