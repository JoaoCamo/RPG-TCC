from openai import OpenAI

client = OpenAI()

def generate_image(image_context: str):
    response = client.images.generate(
        model="gpt-image-1-mini",
        prompt=f"A pixelated image about: {image_context}",
        quality="low",
        size="1024x1024"
    )

    image_b64 = response.data[0].b64_json
    return image_b64