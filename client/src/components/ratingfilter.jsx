import {Checkbox} from "@/components/ui/checkbox";
import Star from "@/assets/star.svg";


export default function RatingFilter(){

    return (
        <>
            <h1 className="mb-2">Ratings</h1>
            <div className="flex items-center">
                <Checkbox value="five"/>
                <label className="flex ">
                    <Star fill="black" width="20" height="20"/>
                    <Star fill="black" width="20" height="20"/>
                    <Star fill="black" width="20" height="20"/>
                    <Star fill="black" width="20" height="20"/>
                    <Star fill="black" width="20" height="20"/>
                </label>
            </div>
            <div className="flex items-center">
                <Checkbox value="four"/>
                <label className="flex ">
                    <Star fill="black" width="20" height="20"/>
                    <Star fill="black" width="20" height="20"/>
                    <Star fill="black" width="20" height="20"/>
                    <Star fill="black" width="20" height="20"/>
                </label>
            </div>
            <div className="flex items-center">
                <Checkbox value="three"/>
                <label className="flex ">
                    <Star fill="black" width="20" height="20"/>
                    <Star fill="black" width="20" height="20"/>
                    <Star fill="black" width="20" height="20"/>
                </label>
            </div>
            <div className="flex items-center">
                <Checkbox value="two"/>
                <label className="flex ">
                    <Star fill="black" width="20" height="20"/>
                    <Star fill="black" width="20" height="20"/>
                </label>
            </div>
            <div className="flex items-center">
                <Checkbox value="one"/>
                <label className="flex ">
                    <Star fill="black" width="20" height="20"/>
                </label>
            </div>
        </>
    )
}