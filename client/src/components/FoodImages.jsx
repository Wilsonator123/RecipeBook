import * as React from "react"

import Image from "next/image"
import {
    Carousel,
    CarouselContent,
    CarouselItem,
    CarouselNext,
    CarouselPrevious,
} from "@/components/ui/carousel"

export function CarouselSize({images}) {
    console.log(images)
    return (
        <Carousel
            opts={{
                align: "start",
            }}
            className="w-[90%] border"
        >
            <CarouselContent className="w-2/3 max-h-[500px] min-h-[300px] p-2">
                {images?.map((image, index) => (
                    <CarouselItem key={index} className="h-[350px] w-2/3 rounded">
                        <div className="relative p-1 h-full w-full">
                            <Image loader={() => image} src={image} layout="fill" objectFit="contain" alt={"Cake"} />
                        </div>
                    </CarouselItem>
                ))}
            </CarouselContent>
            <CarouselPrevious />
            <CarouselNext />
        </Carousel>
    )
}
