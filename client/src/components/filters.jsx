'use client'

import { Separator } from "@/components/ui/separator"
import {Input} from "@/components/ui/input";
import RatingFilter from "@/components/ratingfilter";
import {Button} from "@/components/ui/button";
import Tag from "@/components/tag"
import {useState} from "react";
import {DatePicker} from "@/components/datepicker";



export default function Filters(){
    const [tag, setTag] = useState("");
    const [tags, setTags] = useState([]);
    const handleSubmit = (e) => {
        e.preventDefault()
        setTags([...tags, tag])
        setTag("")
    }

    const handleDelete = (index) => {
        setTags(tags.filter((tag, i) => i !== index));
    }

    return (
        <div className="h-full items-start w-full m-5 gap-5 flex flex-col">
            <div className="flex flex-col gap-2">
                <RatingFilter />
            </div>
            <Separator/>
            <div className="w-full gap-5 flex flex-col">
                <h1 className="mb-2">Tags</h1>
                <form onSubmit={handleSubmit}>
                    <div className="flex w-full max-w-sm items-center space-x-2">
                        <Input className="w-4/5" type="text" placeholder="Enter a tag" value={tag} onChange={(e) => setTag(e.target.value)}/>
                        <Button className="w-1/5 text-white" type="submit">Add</Button>
                    </div>
                </form>

                {tags.length > 0 &&
            <div className="border p-2 flex gap-3 w-full max-h-[100px] rounded">
                {tags.map((tag, index) => (
                    <Tag value={tag} key={index} index={index} onClose={handleDelete} />
                ))}
                </div>
                }
            </div>
            <Separator/>
            <div className="flex flex-col gap-2">
                <h1>Date Added</h1>
                <DatePicker/>
            </div>

        </div>

    )
}