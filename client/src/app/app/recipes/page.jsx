'use client'

import { SortBy } from "@/components/sortby";
import {NewButton} from "@/components/NewButton";
import Filters from "@/components/filters";

import FoodItem from "@/components/foodItem";
import {useEffect, useState} from "react";
import {getRecipes} from "@/hooks/recipe";
import Link from "next/link";

export default function Page(){

    const [recipes, setRecipes] = useState([])
    const [loading, setLoading] = useState(true)


    useEffect(() => {
        getRecipes()
            .then((response) => {
                setRecipes(response);
                setLoading(false);
            });
    }, []);

    return (
        <div className="mx-20 my-10 border p-8 h-[80rem] flex flex-col">
            <div className="flex items-center justify-between mb-3">
                <h1 className="text-2xl">Your Recipes</h1>
                <NewButton />
            </div>

            <div className="flex flex-row gap-10">
                <div className="w-1/3 flex items-center flex-col">
                    <div className="flex justify-between items-center w-full border-b h-10">
                        Filters
                    </div>
                </div>

                <div className="flex flex-col items-center w-full">
                    <div className="flex justify-between items-center w-full border-b h-10 mb-4">
                        <h1>Recipes</h1>
                        <SortBy/>
                    </div>
                </div>
            </div>
            <div className="flex flex-row gap-10">
                <div className="flex w-1/3 h-full  border-r">
                    <Filters/>
                </div>
                { loading ? <div>Loading...</div> :
                <div className="grid grid-cols-3 gap-5 h-auto w-full">
                    { recipes.map((recipe, index) => {
                        return (
                            <FoodItem
                                key={index}
                                item={recipe}
                            />
                        );
                    })}
                </div>
                }
            </div>
        </div>
    )
}