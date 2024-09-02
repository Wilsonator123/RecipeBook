import * as React from "react"

import {
    Select,
    SelectContent,
    SelectGroup,
    SelectItem,
    SelectLabel,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import { Button } from '@/components/ui/button'

export function SortBy() {
    return (
        <Select>
            <SelectTrigger className="w-[150px]">
                <SelectValue placeholder="Sort By" />
            </SelectTrigger>
            <SelectContent>
                <SelectGroup>
                    <SelectLabel>Sort By</SelectLabel>
                    <SelectItem value="recent">Most Recent</SelectItem>
                    <SelectItem value="time">Time to Make</SelectItem>
                    <SelectItem value="rating">Rating</SelectItem>
                </SelectGroup>
            </SelectContent>
        </Select>
    )
}