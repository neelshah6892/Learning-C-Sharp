import scrapy
from pathlib import Path

class CoursesSpider(scrapy.Spider):
    name = "courses"
    allowed_domains = ["ondemand.ecornell.com"]
    start_urls = ["https://ondemand.ecornell.com/"]

    def start_requests(self):
        urls = [
            "https://ondemand.ecornell.com/lesson.do?lessonCode=CIPA531OD1",
            "https://ondemand.ecornell.com/lesson.do?lessonCode=ILRHR521OD4",
        ]
        for url in urls:
            yield scrapy.Request(url=url, callback=self.parse)

    def parse(self, response):
        #page = response.url.split("/")[-2]
        page = response
        filename = f"courses-{page}.html"
        Path(filename).write_bytes(response.body)
        self.log(f"Saved file {filename}")
