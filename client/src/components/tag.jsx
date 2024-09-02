import Close from "@/assets/close.svg";

export default function Tag(){
    return (
        <div className="flex rounded-3xl text-white px-1 gap-1 items-center justify-center bg-secondary">
            <Close width="15" height="15"/>
            <h1 className="pr-1">Tag</h1>
        </div>
    )
}