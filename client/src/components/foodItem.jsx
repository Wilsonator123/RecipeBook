import {Card, CardContent, CardFooter} from "@/components/ui/card";
import Image from "next/image";

export default function FoodItem({title, picture})
{

    return (
    <div className="w-1/3 h-[300px]">
            <Card className="h-full w-full">
                <CardContent className="h-4/5 w-full p-1 mt-1 flex justify-center items-center">
                    <Image alt="food" src={picture} className="h-full w-[90%]"/>
                </CardContent>
                <CardFooter className="w-full border-t py-1 px-3">
                    <p>{title}</p>
                </CardFooter>
            </Card>
        </div>
    )
}