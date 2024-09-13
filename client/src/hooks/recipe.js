
"use server";
import axios from "axios";
import { cookies } from "next/headers";

export const getRecipes = async () => {
    "use server";
    try {
        const id = cookies().get("userid");
        if (!id) return false;

        const response = await axios.get(
            "http://localhost:5000/recipe/getRecipes",
            {
                withCredentials: true,
                headers: {
                    "Content-Type": "application/json",
                    "Cookie": "userid=" + id.value,
                },
            }
        );
        return response.data.Data;
    } catch (error) {
        console.error(error);
        return [];
    }
};

export const getRecipe = async (id) => {
    "use server";
    try {
        const userid = cookies().get("userid");
        if (!userid || !id.recipeid) return false;

        const response = await axios.get(
            "http://localhost:5000/recipe/getRecipe/" + id.recipeid,
            {
                withCredentials: true,
                headers: {
                    "Content-Type": "application/json",
                    "Cookie": "userid=" + userid.value,
                },
            }
        );
        return response.data.Data;
    } catch (error) {
        console.error(error);
        return [];
    }
};


export const recipeByWebsite = async (url) => {
    "use server";
    try{
        const userid = cookies().get("userid");
        if (!userid || !url) return false;

        const response = await axios.post(
            "http://localhost:5000/recipe/webscrape",
            {
                Url: url
            },
            {
                withCredentials: true,
                headers: {
                    "Content-Type": "application/json",
                    "Cookie": "userid=" + userid.value,
                },
            }
        );
        return response.data.Data;
    } catch (error) {
        console.error(error);
        return false;
    }
}

