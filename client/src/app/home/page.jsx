import Image from 'next/image';
import Link from 'next/link';
import Hero from '@/assets/hero.png';
import Second from '@/assets/AIFood.png'
export default function Page() {

    return (
        <>
            <div className="flex justify-center items-center m-20 my-[120px] gap-20">
                <div className="w-1/3">
                    <h1 className="text-5xl pb-7">All your recipes
                        in one place</h1>
                    <h2 className="text-2xl">Intuitive tools to make recipe hunting, storing and cooking easier</h2>
                    <div className="flex mt-7 gap-5">
                        <Link href="/app" className="bg-primary text-white rounded-3xl px-4 py-2">Get Started</Link>
                        <Link href="#"
                              className="border border-primary text-text bg-transparent rounded-3xl px-4 py-2 hover:bg-primary">More
                            Info</Link>
                    </div>
                </div>
                <div className="w-1/2">
                    <Image src={Hero} alt="logo"/>
                </div>
            </div>
            <div className="mx-20 relative flex items-center min-h-[45rem]">
                <div className="absolute left-20 bg-[#F7F7F7] border w-[850px] h-[550px] ">

                </div>

                <div className="flex bg-white absolute z-10 w-[500px] h-[450px] p-4 flex-col right-[20%] border border-primary">
                    <h1 className="text-3xl pb-5 leading-[50.94px]">Import recipes from websites, book and more</h1>
                    <p className="text-xl">RecipeBook allows you to import your favourite recipes from websites, videos
                        and books where they can be stored, edited and viewed.
                        <br/>
                        <br/>
                        Use our tools to save all your favourite recipes in one place, and have them available on all your
                        devices
                    </p>
                </div>
            </div>
            <div className="mx-20 relative flex items-center min-h-[45rem]">
                <div className="absolute right-20 bg-[#F7F7F7] border w-[850px] h-[550px] object-fill">
                    <Image src={Second}  alt="Turning ingredients into meals with AI " width="850" height="550" />
                </div>

                <div className="flex bg-white absolute z-10 w-[500px] h-[450px] p-4 flex-col left-[20%] border border-primary">
                    <h1 className="text-3xl pb-5 leading-[50.94px]">Use AI to turn your ingredients into meals</h1>
                    <p className="text-xl">RecipeBook personalized AI is used to turn your random ingredients into delicious and nutritious meals
                        <br/><br/>
                        Our AI can adapt to user tastes, price points as well as time frames and cooking ability.
                    </p>
                </div>
            </div>
        </>
    )
}