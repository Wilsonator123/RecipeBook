import { SortBy } from "@/components/sortby";
import {NewButton} from "@/components/NewButton";
import Filters from "@/components/filters";

import ex1 from "@/assets/ex1.png"
import ex2 from "@/assets/ex2.png"
import ex3 from "@/assets/ex3.jpg"

import FoodItem from "@/components/foodItem";

const food = [
    {
        "title": "Spaghetti Carbonara",
        "picture": ex1,
    },
    {
        "title": "Pizza",
        "picture": ex2
    },
    {
        "title": "Butter Chicken",
        "picture": ex3
    }
]

export default function Page(){

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
            <div className="flex flex-row gap-10 h-full">
                <div className="flex w-1/3 h-full  border-r">
                    <Filters/>
                </div>
                <div className="flex gap-5 w-full">
                    {
                        food.map((item, index) =>
                            (
                                <FoodItem title={item.title} picture={item.picture} key={index}/>
                            ))
                    }
                </div>
            </div>
        </div>
    )
}