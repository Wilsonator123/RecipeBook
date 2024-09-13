'use client'

import { useEffect, useState } from "react";
import { getRecipe } from "@/hooks/recipe";
import Image from 'next/image';
import Edit from "@/assets/edit.svg";
import Rating from "@/components/rating";
import Tag from "@/components/tag";
import {Button} from "@/components/ui/button";
import {CarouselSize} from "@/components/FoodImages";

export default function Page({params}) {

    const [recipe, setRecipe] = useState([])

    const [loading, setLoading] = useState(false)

    useEffect(() => {
        getRecipe(params)
            .then((response) => {
                setRecipe(response);
                setLoading(true);
            });
    }, [params]);

    return (
        <>
        {loading &&
            <div className="mx-20 my-10 border p-8 min-h-[60rem] flex gap-10">
                <div className="border w-1/4 min-h-[20rem] flex flex-col gap-5">
                    <div className="h-[250px] border flex justify-center">
                        <div className="w-4/5 h-full relative">
                            {recipe.Images && recipe.Images.length > 0 && (
                                <Image loader={() => recipe.Images[0]} src={recipe.Images[0]} objectFit="contain" layout="fill" alt="Photo" className="w-full h-full"/>
                            )}
                        </div>
                    </div>
                    <div className="h-1/5">
                        <div className="flex gap-2">
                            <h1 className="underline mb-2">Ingredients </h1>
                            -
                            <h1>Serves {recipe.Serves}</h1>
                        </div>
                        <ul className="flex flex-col gap-2">
                            {recipe.Ingredients.map((ingredient, index) => {
                                return (
                                    <li key={index}>{ingredient.Quantity !== "none" && ingredient.Quantity} {ingredient.Unit !== "none" && ingredient.Unit} {ingredient.Name}</li>
                                )
                            })}
                        </ul>
                    </div>
                </div>
                <div className="flex w-3/4 min-h-[20rem] border p-5 flex-col">
                <div className="w-full h-[150px] border-b mb-5">
                        <div className="flex justify-between mb-2">
                            <div className="flex items-center gap-5">
                                <h1 className="text-3xl">{recipe.Name}</h1>
                                <Rating rating={recipe.Rating}/>
                            </div>
                            <Button variant="outline" className="flex gap-1 items-center">
                                Edit
                                <Edit fill="black" width="20" height="20" className="mb-1"/>
                            </Button>
                        </div>
                        <div className="flex gap-2">
                            {
                                recipe.Tags?.map((tag, index) => {
                                    return (
                                        <Tag key={index} value={tag} className="px-1.5"/>
                                    )
                                })
                            }
                        </div>
                        <div className="flex flex-col my-3 gap-2">
                            <h1>Prep Time: {recipe.PrepTime} mins</h1>
                            <h1>Cook Time: {recipe.CookTime} mins</h1>
                        </div>

                    </div>
                    <div className="flex flex-col h-full w-full justify-between">
                        <div className="flex flex-col gap-8 mb-10">
                            <h1 className="text-2xl">Instructions</h1>
                            <div className="flex flex-col gap-10">
                                {
                                    recipe.Instructions.map((line, index) => {
                                        return (
                                            <div key={index} className="w-full h-1/5">
                                                <h1>{index + 1}. {line}</h1>
                                            </div>
                                        )
                                    })
                                }
                            </div>
                        </div>
                        {recipe.Images && recipe.Images.length > 0 &&
                            <div className="align-center flex justify-center min-h-[300px] items-center w-full">
                                <CarouselSize className="w-full" images={recipe.Images}/>
                            </div>
                        }
                    </div>
                </div>
            </div>
        }
        </>

    )


}