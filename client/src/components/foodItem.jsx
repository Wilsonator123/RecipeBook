import {Card, CardContent, CardFooter} from "@/components/ui/card";
import Image from "next/image";
import {useState} from "react";
import Link from "next/link";

export default function FoodItem({item}) {

    const [recipe, setRecipe] = useState(item)

    return (
    <Link href={`/app/recipes/${recipe.Id}`} className="w-full h-[300px]">
            <Card className="h-full w-full overflow-hidden">
                <CardContent className="relative h-4/5 w-full p-1 mt-1 flex justify-center items-center">
                    {recipe.Images && recipe.Images.length > 0 && (
                    <Image loader={() => recipe?.Images[0]} src={recipe.Images[0]} objectFit="contain" layout="fill" alt="Photo" className="w-full h-full"/>
                        )}
                </CardContent>
                <CardFooter className="flex overflow-hidden w-full border-t py-1 px-3">
                    <p>{recipe.Name}</p>
                </CardFooter>
            </Card>
        </Link>
    )
}